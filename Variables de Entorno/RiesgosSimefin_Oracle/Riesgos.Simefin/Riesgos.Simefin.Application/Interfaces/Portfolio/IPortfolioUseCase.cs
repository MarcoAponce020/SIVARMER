using Microsoft.AspNetCore.Http;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.Portfolio;

namespace Riesgos.Simefin.Application.Interfaces.Portfolio
{

    /// <summary>
    /// Interfaz que define los métodos de lógica de negocios para Portafolios
    /// </summary>
    public interface IPortfolioUseCase
    {

        /// <summary>
        /// Obtener todos los portafolios
        /// </summary>
        /// <returns></returns>
        Task<ResponseDTO> GetAllPortfolio();

        /// <summary>
        /// Obtener portafolio por una Fecha
        /// </summary>
        /// <param name="positionDate">Fecha de Posición</param>
        /// <returns></returns>
        Task<ResponseDTO> GetPortfolioByDate(string positionDate);

        /// <summary>
        /// Obtener portafolio por Identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        Task<ResponseDTO> GetPortfolioById(int id);

        /// <summary>
        /// Crear nuevo Portafolio
        /// </summary>
        /// <param name="request">Datos del Portafolio</param>
        /// <returns></returns>
        Task<ResponseDTO> CreatePortfolio(PortafolioCreateDTO request);

        /// <summary>
        /// Actualización de un portafolio
        /// </summary>
        /// <param name="portafolioId">Identificador del Portafolio</param>
        /// <param name="request">Contenedor con información de Portafolio</param>
        /// <returns></returns>
        Task<ResponseDTO> UpdatePortfolio(int portafolioId, PortafolioUpdateDTO request);

        /// <summary>
        /// Eliminar un Portafolio
        /// </summary>
        /// <param name="id">Identificador de Portafolio</param>
        /// <returns></returns>
        Task<ResponseDTO> DeletePortfolio(int id);

        /// <summary>
        /// Carga de archivo de excel con registros de portafolios
        /// </summary>
        /// <param name="file">Archivo de Excel</param>
        /// <returns></returns>
        Task<ResponseDTO> ExcelLoad(IFormFile file);

        /// <summary>
        /// Obtener archivo almacenado en BD en formato Base64
        /// </summary>
        /// <param name="fileDate">Fecha de almacenamiento</param>
        /// <returns></returns>
        Task<ResponseDTO> GetFileCSV(string fileDate);

    }

}
