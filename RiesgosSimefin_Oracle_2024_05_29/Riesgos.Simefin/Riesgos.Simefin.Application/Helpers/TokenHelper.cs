using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Riesgos.Simefin.Application.DTOs.User;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.Interfaces.Authorization;
using Riesgos.Simefin.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Riesgos.Simefin.Application.Helpers
{

    public class TokenHelper
    {

        /// <summary>
        /// Generar token de acceso
        /// </summary>
        /// <param name="user">Contenedor con datos de Usuario</param>
        /// <param name="tokenSecretKey">Clave secreta</param>
        /// <param name="accessTokenTime">Tiempo de vida en minutos del AccessToken</param>
        /// <returns></returns>
        public static string GenerateAccessToken(Usuario user, string tokenSecretKey, int accessTokenTime)
        {
            var key = Encoding.ASCII.GetBytes(tokenSecretKey);
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
        public static string GenerateRefreshToken()
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
        /// <param name="userId">Identificador de Usuario</param>
        /// <param name="accessToken">Cadena con el AccessToken</param>
        /// <param name="refreshToken">Cadena con el RefreshToken</param>
        /// <param name="refreshTokenTime">Tiempo de vida en minutos del RefreshToken</param>
        /// <returns></returns>
        public static async Task<ResponseDTO> SaveRefreshToken(int userId, string accessToken, string refreshToken, int refreshTokenTime, IAuthorizationRepository authorizationRepository)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                var historialRefreshToken = new Token
                {
                    UsuarioId = userId.ToString(),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpirationDate = DateTime.UtcNow.AddMinutes(refreshTokenTime),
                    //isValid = true //Ahora es campo calculado
                };

                var token = await authorizationRepository.AddAsync(historialRefreshToken);

                response.IsExitoso = token > 0;
                response.Mensaje = response.IsExitoso ? "Token generado con éxito." : "El token no pudo ser creado.";
                response.Resultado = new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

    }

}
