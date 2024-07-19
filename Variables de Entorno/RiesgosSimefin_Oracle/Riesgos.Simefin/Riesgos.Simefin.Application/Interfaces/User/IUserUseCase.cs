using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.User;

namespace Riesgos.Simefin.Application.Interfaces.User
{

    /// <summary>
    /// Interfaz que define los métodos de lógica de negocios para Usuarios
    /// </summary>
    public interface IUserUseCase
    {

        /// <summary>
        /// Obtener token a partir de credenciales de usuario
        /// </summary>
        /// <param name="request">Contenedor con datos del usuario</param>
        /// <returns></returns>
        Task<ResponseDTO> Login(LoginRequestDTO request);

        /// <summary>
        /// Crear nuevo usuario
        /// </summary>
        /// <param name="request">Contenedor con datos del usuario</param>
        /// <returns></returns>
        Task<ResponseDTO> Register(RegistroRequestDTO request);

    }

}
