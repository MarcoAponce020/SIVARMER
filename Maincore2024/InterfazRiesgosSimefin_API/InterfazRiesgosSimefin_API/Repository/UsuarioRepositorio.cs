using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using InterfazRiesgosSimefin_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterfazRiesgosSimefin_API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        private readonly int accessToken = 0;
        private readonly int refreshToken = 0;

        public UsuarioRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            accessToken = int.Parse(configuration.GetValue<string>("ApiSettings:AccessToken"));
            refreshToken = int.Parse(configuration.GetValue<string>("ApiSettings:RefreshToken"));

        }

        public bool IsUsuarioUnico(string userName)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if (usuario == null)
            {
                return false;
            }
            return false;
        }

        public async Task<TokenDTO> Login(LoginRequestDTO userDTO)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.UserName.Trim().ToLower() == userDTO.Username.Trim().ToLower() &&
                                                                      u.Password.Trim() == userDTO.Password.Trim());

            if (usuario == null)
            {
                return new TokenDTO()
                {
                    AccessToken = "",
                    RefreshToken = ""

                    //Usuario = null
                };
            }

            var tokenId = $"JTI{Guid.NewGuid()}";
            var accessToken = await GetAccessToken(usuario, tokenId);
            var refreshToken = await CreateNewRefreshToken(usuario.Id.ToString(), tokenId);
            TokenDTO tokenDTO = new()
            {
                AccessToken = accessToken, //Obtiene el token creado
                RefreshToken = refreshToken
                // Usuario = usuario,
            };
            return tokenDTO;
        }


        public async Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO)
        {
            Usuario usuario = new()
            {
                UserName = registroRequestDTO.UserName,
                Password = registroRequestDTO.Password,
                Nombre = registroRequestDTO.Nombre,
                Rol = registroRequestDTO.Rol
            };
            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = "";
            return usuario;
        }



        private async Task<string> GetAccessToken(Usuario usuario, string tokenId)
        {
            //Si Usuario Existe Generamos el JW Token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(ClaimTypes.Role, usuario.Rol),
                   new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(this.accessToken),//Tiempo que expira el Token
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);
            return tokenStr;
        }


        public async Task<TokenDTO> RefreshAccessToken(TokenDTO tokenDTO)
        {
            //Encontrar un refresh token existente
            var existeRefreshToken = await _db.refreshTokens.FirstOrDefaultAsync(u => u.refreshToken == tokenDTO.RefreshToken && u.isValid == true);
            if (existeRefreshToken == null)
            {
                return new TokenDTO();
            }


            //Se comparan los datos del token de acceso y actualización existente proporcionada y, si no coinciden entonces se considera como corrupto el token
            var accessTokenData = GetAccessTokenData(tokenDTO.AccessToken);
            if (!accessTokenData.isExitoso || accessTokenData.idUsuario != existeRefreshToken.idUsuario
                || accessTokenData.tokenId != existeRefreshToken.tokenId)
            {
                existeRefreshToken.isValid = false;
                _db.SaveChanges();
                return new TokenDTO();

            }

            if (!existeRefreshToken.isValid)
            {
                var chainRecords = _db.refreshTokens.Where(u => u.idUsuario == existeRefreshToken.idUsuario
                && u.tokenId == existeRefreshToken.tokenId);

                var count = chainRecords.Count(x => int.Parse(x.idUsuario) == 1);

                bool isValid = false;
                if (count < 6)
                {
                    isValid = true;

                }
                else
                {
                    foreach (var item in chainRecords)
                    {
                        item.isValid = false;
                    }
                    _db.UpdateRange(chainRecords);
                    _db.SaveChanges();
                }


                return tokenDTO;
            }


            //si acaba de expirar, se marca como no válido y regresa vacío
            if (existeRefreshToken.TiempoExpiracion < DateTime.UtcNow)
            {
                existeRefreshToken.isValid = false;
                _db.SaveChanges();
                return new TokenDTO();
            }
            //Remplaza el token viejo por uno nuevo y actualiza la fecha de expiracion
            var newRefreshToken = await CreateNewRefreshToken(existeRefreshToken.idUsuario, existeRefreshToken.tokenId);

            existeRefreshToken.isValid = false;
            _db.SaveChanges();


            var usuarioAplicacion = _db.Usuarios.FirstOrDefault(u => u.Id.ToString() == existeRefreshToken.idUsuario);
            if (usuarioAplicacion == null)
                return new TokenDTO();

            //Generar un nuevo access token 
            var newAccessToken = await GetAccessToken(usuarioAplicacion, existeRefreshToken.tokenId);

            return new TokenDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }


        private async Task<string> CreateNewRefreshToken(string idUsuario, string tokenId)
        {
            RefreshToken refreshToken = new()
            {
                isValid = true,
                idUsuario = idUsuario,
                tokenId = tokenId,
                TiempoExpiracion = DateTime.UtcNow.AddMinutes(this.refreshToken),//Tiempo que expira el token
                refreshToken = Guid.NewGuid().ToString()
            };
            await _db.refreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();
            return refreshToken.refreshToken;
        }


        private (bool isExitoso, string idUsuario, string tokenId) GetAccessTokenData(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);
                var tokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti).Value;
                var idUsuario = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value;
                return (true, idUsuario, tokenId);
            }
            catch
            {

                return (false, null, null);
            }

        }



    }
}
