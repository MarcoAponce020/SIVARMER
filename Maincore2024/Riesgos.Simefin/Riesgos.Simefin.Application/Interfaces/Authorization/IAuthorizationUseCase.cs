using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.User;

namespace Riesgos.Simefin.Application.Interfaces.Authorization
{

    /// <summary>
    /// Interfaz que define los métodos de lógica de negocios para Tokens
    /// </summary>u
    public interface IAuthorizationUseCase
    {

        /// <summary>
        /// Generar Refresh Token
        /// </summary>
        /// <param name="request">Contenedor con datos del usuario</param>
        /// <returns></returns>
        Task<ResponseDTO> GenerateRefreshAccessToken(TokenDTO request);

    }

}
