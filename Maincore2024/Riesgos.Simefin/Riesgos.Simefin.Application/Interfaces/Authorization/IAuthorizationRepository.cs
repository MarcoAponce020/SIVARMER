using Riesgos.Simefin.Domain.Entities;

namespace Riesgos.Simefin.Application.Interfaces.Authorization
{

    /// <summary>
    /// Interfaz que define los métodos de acceso a datos para Tokens
    /// </summary>
    public interface IAuthorizationRepository
    {

        /// <summary>
        /// Agrega un nuevo Token
        /// </summary>
        /// <param name="entity">Contenedor con información del Token</param>
        /// <returns></returns>
        Task<int> AddAsync(Token entity);

        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Obtener todos los Tokens
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Token>> GetAllAsync();

        /// <summary>
        /// Obtener un Token a través de un identificador
        /// </summary>
        /// <param name="tokenId">Identificador</param>
        /// <returns></returns>
        Task<Token> GetByIdAsync(int tokenId);

        /// <summary>
        /// Actualizando información de un Token
        /// </summary>
        /// <param name="entity">Contenedor con información de Token</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Token entity);

    }

}
