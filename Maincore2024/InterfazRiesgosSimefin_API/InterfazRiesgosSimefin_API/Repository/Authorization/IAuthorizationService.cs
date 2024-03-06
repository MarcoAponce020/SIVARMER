using InterfazRiesgosSimefin_API.Models.Dto;
using InterfazRiesgosSimefin_API.Models;

namespace InterfazRiesgosSimefin_API.Repository.User
{

    public interface IAuthorizationService
    {

        /// <summary>
        /// Obtener token a partir de credenciales de usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<APIResponse> Login(LoginRequestDTO modelRequest);

        /// <summary>
        /// Registrar usuario
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        Task<APIResponse> Register(RegistroRequestDTO modelRequest);

        /// <summary>
        /// Generar Refresh Token
        /// </summary>
        /// <param name="tokenDTO"></param>
        /// <returns></returns>
        Task<APIResponse> GenerateRefreshAccessToken(TokenDTO tokenDTO);

    }

}
