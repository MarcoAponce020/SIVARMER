using Riesgos.Simefin.Domain.Entities;

namespace Riesgos.Simefin.Application.Interfaces.User
{

    /// <summary>
    /// Interfaz que define los métodos de acceso a datos para Usuarios
    /// </summary>
    public interface IUserRepository
    {

        /// <summary>
        /// Agregar nuevo usuario
        /// </summary>
        /// <param name="entity">Contenedor con información del usuario</param>
        /// <returns></returns>
        Task<int> AddAsync(Usuario entity);

        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Usuario>> GetAllAsync();

        /// <summary>
        /// Obtener información de un usuario
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        Task<Usuario> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Usuario entity);

    }

}
