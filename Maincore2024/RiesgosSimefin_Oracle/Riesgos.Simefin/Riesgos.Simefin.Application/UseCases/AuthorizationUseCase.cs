using Microsoft.Extensions.Configuration;
using Riesgos.Simefin.Application.DTOs.User;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.Helpers;
using Riesgos.Simefin.Application.Interfaces.Authorization;
using Riesgos.Simefin.Application.Interfaces.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Riesgos.Simefin.Application.UseCases
{

    public class AuthorizationUseCase : IAuthorizationUseCase
    {

        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthorizationUseCase(IAuthorizationRepository authorizationRepository, IConfiguration configuration, IUserRepository userRepository)
        {
            _authorizationRepository = authorizationRepository;
            _configuration = configuration;
            _userRepository = userRepository;
        }



        /// <summary>
        /// Generar Refresh Token
        /// </summary>
        /// <param name="request">Contenedor con datos del usuario</param>
        /// <returns></returns>
        public async Task<ResponseDTO> GenerateRefreshAccessToken(TokenDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                //var refreshTokenExpired = await _dbCtx.refreshTokens.AsNoTracking().FirstOrDefaultAsync(x => x.refreshToken == tokenDTO.RefreshToken && x.isValid == true);
                var allTokens = await _authorizationRepository.GetAllAsync();
                var refreshTokenExpired = allTokens.FirstOrDefault(x => x.RefreshToken == request.RefreshToken && x.IsValid == true);
                if (refreshTokenExpired == null)
                {
                    response.IsExitoso = false;
                    response.Mensaje = "No existe un RefreshToken.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var expiredToken = tokenHandler.ReadJwtToken(request.AccessToken);
                if (expiredToken.ValidTo > DateTime.UtcNow)
                {
                    response.IsExitoso = false;
                    response.Mensaje = "Token no ha expirado.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var refreshTokenCreated = TokenHelper.GenerateRefreshToken();

                //var user = await _dbCtx.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == int.Parse(refreshTokenExpired.idUsuario));
                var user = await _userRepository.GetByIdAsync(int.Parse(refreshTokenExpired.UsuarioId!));

                string secretKey = _configuration.GetValue<string>("ApiSettings:Secret")!;
                int accessTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:AccessTokenTime")!);
                var tokenCreated = TokenHelper.GenerateAccessToken(user, secretKey, accessTokenTime);

                //Actualizando el tiempo de expiración del token viejo
                refreshTokenExpired.ExpirationDate = DateTime.MinValue;
                //_dbCtx.Entry(refreshTokenExpired).State = EntityState.Modified;
                //await _dbCtx.SaveChangesAsync();

                var updateToken = await _authorizationRepository.UpdateAsync(refreshTokenExpired);

                int refreshTokenTime = int.Parse(_configuration.GetValue<string>("ApiSettings:RefreshTokenTime")!);
                response = await TokenHelper.SaveRefreshToken(user.Id, tokenCreated, refreshTokenCreated, refreshTokenTime, _authorizationRepository);
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = ExceptionMessageHelper.GetExceptionMessage(ex);
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

    }

}
