using AutoMapper;
using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InterfazRiesgosSimefin_API.Services.Authorization
{

    public class AuthorizationService : IAuthorizationService
    {

        private readonly ApplicationDbContext _dbCtx;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="configuration"></param>
        public AuthorizationService(ApplicationDbContext db, IConfiguration configuration, IMapper mapper)
        {
            _dbCtx = db;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtener token a partir de credenciales de usuario
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        public async Task<APIResponse> Login(LoginRequestDTO modelRequest)
        {
            APIResponse response = new APIResponse();

            try
            {
                var user = await _dbCtx.Usuarios.AsNoTracking()
                                                .FirstOrDefaultAsync(u => u.UserName.Trim().ToLower() == modelRequest.Username.Trim().ToLower() &&
                                                                          u.Password.Trim() == modelRequest.Password.Trim());

                if (user == null)
                {
                    return new APIResponse()
                    {
                        IsExitoso = false,
                        Mensaje = "Usuario o Contraseña incorrectos.",
                        statusCode = HttpStatusCode.BadRequest
                    };
                }

                string tokenCreated = GenerateAccessToken(user);
                string refreshTokenCreated = GenerateRefreshToken();

                response = await SaveRefreshToken(user.Id, tokenCreated, refreshTokenCreated);

                var userData = _mapper.Map<UserDTO>(user);
                var tokenData = (TokenDTO)response.Resultado;

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
                response.Mensaje = GetExceptionMessage(ex);
                response.statusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Generar Refresh Token
        /// </summary>
        /// <param name="tokenDTO"></param>
        /// <returns></returns>
        public async Task<APIResponse> GenerateRefreshAccessToken(TokenDTO tokenDTO)
        {
            APIResponse response = new APIResponse();

            try
            {
                var refreshTokenExpired = await _dbCtx.refreshTokens.AsNoTracking().FirstOrDefaultAsync(x => x.refreshToken == tokenDTO.RefreshToken && x.isValid == true);
                if (refreshTokenExpired == null)
                {
                    response.IsExitoso = false;
                    response.ErrorMessages.Add("No existe un RefreshToken.");
                    response.statusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var expiredToken = tokenHandler.ReadJwtToken(tokenDTO.AccessToken);
                if (expiredToken.ValidTo > DateTime.UtcNow)
                {
                    response.IsExitoso = false;
                    response.ErrorMessages.Add("Token no ha expirado.");
                    response.statusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var refreshTokenCreated = GenerateRefreshToken();

                var user = await _dbCtx.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == int.Parse(refreshTokenExpired.idUsuario));
                var tokenCreated = GenerateAccessToken(user);

                //Actualizando el tiempo de expiración del token viejo
                refreshTokenExpired.TiempoExpiracion = DateTime.MinValue;
                _dbCtx.Entry(refreshTokenExpired).State = EntityState.Modified;
                await _dbCtx.SaveChangesAsync();

                response = await SaveRefreshToken(user.Id, tokenCreated, refreshTokenCreated);
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = GetExceptionMessage(ex);
                response.statusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        /// <summary>
        /// Registrar usuario
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponse> Register(RegistroRequestDTO modelRequest)
        {
            APIResponse response = new APIResponse();

            try
            {
                bool isUsuarioUnico = IsUsuarioUnico(modelRequest.UserName);

                if (isUsuarioUnico)
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.IsExitoso = false;
                    response.ErrorMessages.Add("El Usuario ya existe.");
                    response.statusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                Usuario usuario = new()
                {
                    Nombre = modelRequest.Nombre,
                    Password = modelRequest.Password,
                    Rol = modelRequest.Rol,
                    UserName = modelRequest.UserName
                };

                await _dbCtx.Usuarios.AddAsync(usuario);
                await _dbCtx.SaveChangesAsync();
                usuario.Password = "";

                response.IsExitoso = true;
                response.Mensaje = "Usuario generado con éxito.";
                response.Resultado = usuario;
                response.statusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = GetExceptionMessage(ex);
                response.statusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }


        #region Métodos privados ...

        /// <summary>
        /// Generar token de acceso
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GenerateAccessToken(Usuario user)
        {
            var secretKey = _configuration.GetValue<string>("ApiSettings:Secret");
            int accessTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:AccessTokenTime"));
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Rol));

            var tokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(accessTokenTime),//Tiempo que expira el Token
                SigningCredentials = tokenCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenCreated = tokenHandler.WriteToken(token);

            return tokenCreated;
        }

        /// <summary>
        /// Generar token de actualización
        /// </summary>
        /// <returns></returns>
        private string GenerateRefreshToken()
        {
            var content = new byte[64];
            string refreshToken = string.Empty;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(content);
                refreshToken = Convert.ToBase64String(content);
            }

            return refreshToken;
        }

        /// <summary>
        /// Guardando tokens
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private async Task<APIResponse> SaveRefreshToken(int userId, string accessToken, string refreshToken)
        {
            APIResponse response = new APIResponse();
            int refreshTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:RefreshTokenTime"));
            var historialRefreshToken = new RefreshToken
            {
                idUsuario = userId.ToString(),
                tokenId = accessToken,
                refreshToken = refreshToken,
                TiempoExpiracion = DateTime.UtcNow.AddMinutes(refreshTokenTime),
                //isValid = true //Ahora es campo calculado
            };

            await _dbCtx.refreshTokens.AddAsync(historialRefreshToken);
            await _dbCtx.SaveChangesAsync();

            response.IsExitoso = true;
            response.Mensaje = "Token generado con éxito.";
            response.Resultado = new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
            response.statusCode = HttpStatusCode.OK;

            return response;
        }

        /// <summary>
        /// Recuperar mensajes de error anidados
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private string GetExceptionMessage(Exception error)
        {
            string message = error.Message + " ";
            while (error?.InnerException != null)
            {
                message += error.InnerException?.Message;
                error = error.InnerException;
            }

            return message;
        }

        /// <summary>
        /// Verifica que el userName sea único
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsUsuarioUnico(string userName)
        {
            var usuario = _dbCtx.Usuarios.AsNoTracking().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if (usuario == null)
            {
                return false;
            }
            return false;
        }

        #endregion

    }


}
