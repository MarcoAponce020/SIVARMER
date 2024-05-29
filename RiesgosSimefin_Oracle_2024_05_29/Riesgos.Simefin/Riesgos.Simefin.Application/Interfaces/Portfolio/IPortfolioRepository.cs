using Riesgos.Simefin.Domain.Entities;

namespace Riesgos.Simefin.Application.Interfaces.Portfolio
{

    /// <summary>
    /// Interfaz que define los métodos de acceso a datos para Portafolios
    /// </summary>
    public interface IPortfolioRepository
    {

        /// <summary>
        /// Obtener todos los Portafolios
        /// </summary>
        /// <param name="whereFilter">Filtros sobre la consulta</param>
        /// <returns></returns>
        Task<IEnumerable<Portafolio>> GetAllAsync(List<object>? whereFilter = null);

        /// <summary>
        /// Obtener portafolio por Identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        Task<Portafolio> GetByIdAsync(int id);

        /// <summary>
        /// Agregar nuevo portafolio.
        /// </summary>
        /// <param name="entity">Contenedor con información del portafolio.</param>
        /// <returns></returns>
        Task<int> AddAsync(Portafolio entity);

        /// <summary>
        /// Actualización de un portafolio
        /// </summary>
        /// <param name="entity">Contenedor con información de Portafolio</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Portafolio entity);

        /// <summary>
        /// Eliminar un Portafolio
        /// </summary>
        /// <param name="id">Identificador de Portafolio</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Obtener todos los SubPortafolios
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubPortafolios>> GetAllSubPortfoliosAsync();

        /// <summary>
        /// Guardando en BD la lista de portafolios suministrada
        /// </summary>
        /// <param name="portafoliosList">Listado de portafolios</param>
        /// <returns></returns>
        Task<List<string>> SavePortfoliosList(List<Portafolio> portafoliosList);

    }

}
