using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.User;
using Riesgos.Simefin.Application.Helpers;
using Riesgos.Simefin.Application.Interfaces.Authorization;
using Riesgos.Simefin.Application.Interfaces.User;
using Riesgos.Simefin.Domain.Entities;
using System.Net;

namespace Riesgos.Simefin.Application.UseCases
{

    /// <summary>
    /// Clase que implementa los métodos de lógica de Negocios para Usuarios y Tokens
    /// </summary>
    public class UserUseCase : IUserUseCase
    {

        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UserUseCase(IAuthorizationRepository authorizationRepository, IConfiguration configuration, IUserRepository userRepository)
        {
            _authorizationRepository = authorizationRepository;
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        /// <summary>
        /// Obtener token a partir de credenciales de usuario
        /// </summary>
        /// <param name="request">Contenedor con datos del usuario</param>
        /// <returns></returns>
        public async Task<ResponseDTO> Login(LoginRequestDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var allUsers = await _userRepository.GetAllAsync();
                //var user = allUsers.FirstOrDefault(u => u.UserName.Trim().ToLower() == request.Username!.Trim().ToLower() &&
                //                                        u.Password.Trim() == request.Password!.Trim());
                var user = allUsers.FirstOrDefault(u => u.UserName.Trim().ToLower() == request.Username!.Trim().ToLower());
                var result = this.VerifyPassword(user!, request.Password!);

                if (!result)
                {
                    response.IsExitoso = false;
                    response.ErrorMessages.Add("Usuario o Contraseña incorrectos.");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                string secretKey = _configuration.GetValue<string>("ApiSettings:Secret")!;
                int accessTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:AccessTokenTime")!);
                string tokenCreated = TokenHelper.GenerateAccessToken(user, secretKey, accessTokenTime);
                string refreshTokenCreated = TokenHelper.GenerateRefreshToken();

                int refreshTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:RefreshTokenTime")!);
                response = await TokenHelper.SaveRefreshToken(user.Id, tokenCreated, refreshTokenCreated, refreshTokenTime, _authorizationRepository);

                //var userData = _mapper.Map<UserDTO>(user);
                var userData = new UserDTO { 
                    Nombre = user.Nombre,
                    UserName = user.UserName
                };
                var tokenData = (TokenDTO)response.Resultado!;

                response.Resultado = new
                {
                    userName = userData.UserName,
                    nombre = userData.Nombre,
                    accessToken = tokenData.AccessToken,
                    refreshToken = tokenData.RefreshToken
                };
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ExceptionMessageHelper.GetExceptionMessage(ex));
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Crear nuevo usuario
        /// </summary>
        /// <param name="request">Contenedor con datos del usuario</param>
        /// <returns></returns>
        public async Task<ResponseDTO> Register(RegistroRequestDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var isUsuarioUnico = this.IsUsuarioUnico(request.UserName);

                if (isUsuarioUnico)
                {
                    response.IsExitoso = false;
                    response.ErrorMessages.Add("El Usuario ya existe.");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                Usuario usuario = new()
                {
                    Nombre = request.Nombre,
                    Password = request.Password,
                    Rol = request.Rol,
                    UserName = request.UserName
                };

                //Hasheando el password
                usuario.Password = _passwordHasher.HashPassword(usuario, request.Password);

                var userAdd = await _userRepository.AddAsync(usuario);
                usuario.Id = this.GetUserByUserName(request.UserName).Id;

                response.IsExitoso = userAdd > 0;
                response.Mensaje = response.IsExitoso ? "Usuario generado con éxito." : "No fué posible crear el usuario.";
                usuario.Password = "";
                response.Resultado = usuario;
                response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = ExceptionMessageHelper.GetExceptionMessage(ex);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }


        #region Métodos privados ...

        ///////// <summary>
        ///////// Generar token de acceso
        ///////// </summary>
        ///////// <param name="userId"></param>
        ///////// <returns></returns>
        //////private string GenerateAccessToken(Usuario user)
        //////{
        //////    var secretKey = _configuration.GetValue<string>("ApiSettings:Secret");
        //////    int accessTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:AccessTokenTime")!);
        //////    var key = Encoding.ASCII.GetBytes(secretKey!);

        //////    var claims = new ClaimsIdentity();
        //////    claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        //////    claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
        //////    claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        //////    claims.AddClaim(new Claim(ClaimTypes.Role, user.Rol));

        //////    var tokenCredentials = new SigningCredentials(
        //////        new SymmetricSecurityKey(key),
        //////        SecurityAlgorithms.HmacSha256Signature
        //////    );

        //////    var tokenDescriptor = new SecurityTokenDescriptor
        //////    {
        //////        Subject = claims,
        //////        Expires = DateTime.UtcNow.AddMinutes(accessTokenTime),//Tiempo que expira el Token
        //////        SigningCredentials = tokenCredentials,
        //////    };

        //////    var tokenHandler = new JwtSecurityTokenHandler();
        //////    var token = tokenHandler.CreateToken(tokenDescriptor);
        //////    var tokenCreated = tokenHandler.WriteToken(token);

        //////    return tokenCreated;
        //////}

        ///////// <summary>
        ///////// Generar token de actualización
        ///////// </summary>
        ///////// <returns></returns>
        //////private string GenerateRefreshToken()
        //////{
        //////    var content = new byte[64];
        //////    string refreshToken = string.Empty;
        //////    using (var rng = RandomNumberGenerator.Create())
        //////    {
        //////        rng.GetBytes(content);
        //////        refreshToken = Convert.ToBase64String(content);
        //////    }

        //////    return refreshToken;
        //////}

        ///////// <summary>
        ///////// Guardando tokens
        ///////// </summary>
        ///////// <param name="userId"></param>
        ///////// <param name="accessToken"></param>
        ///////// <param name="refreshToken"></param>
        ///////// <returns></returns>
        //////private async Task<ResponseDTO> SaveRefreshToken(int userId, string accessToken, string refreshToken)
        //////{
        //////    ResponseDTO response = new ResponseDTO();

        //////    try
        //////    {
        //////        int refreshTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:RefreshTokenTime")!);
        //////        var historialRefreshToken = new Token
        //////        {
        //////            UsuarioId = userId.ToString(),
        //////            AccessToken = accessToken,
        //////            RefreshToken = refreshToken,
        //////            ExpirationDate = DateTime.UtcNow.AddMinutes(refreshTokenTime),
        //////            //isValid = true //Ahora es campo calculado
        //////        };

        //////        var token = await _authorizationRepository.AddAsync(historialRefreshToken);

        //////        response.IsExitoso = token > 0;
        //////        response.Mensaje = response.IsExitoso ? "Token generado con éxito." : "El token no pudo ser creado.";
        //////        response.Resultado = new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        //////        response.StatusCode = HttpStatusCode.OK;
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        response.IsExitoso = false;
        //////        response.ErrorMessages.Add(ex.Message);
        //////        response.StatusCode = HttpStatusCode.BadRequest;
        //////    }

        //////    return response;
        //////}

        /// <summary>
        /// Obtener información de un usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private Usuario GetUserByUserName(string userName)
        {
            //var usuario = _dbCtx.Usuarios.AsNoTracking().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            var usuario = _userRepository.GetAllAsync().Result.ToList().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            return usuario!;
        }

        /// <summary>
        /// Verifica que el userName sea único
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsUsuarioUnico(string userName)
        {
            //var usuario = _dbCtx.Usuarios.AsNoTracking().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            var usuario = _userRepository.GetAllAsync().Result.ToList().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if (usuario == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verificar que el password encriptado existe o es correcto
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(Usuario user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success;
        }

        #endregion

    }

}
