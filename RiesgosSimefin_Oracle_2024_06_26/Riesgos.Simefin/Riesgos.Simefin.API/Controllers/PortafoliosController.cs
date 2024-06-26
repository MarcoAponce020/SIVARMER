using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.Portfolio;
using Riesgos.Simefin.Application.Interfaces.Portfolio;
using Riesgos.Simefin.Domain.Entities;
using System.Net;
using System.Text;

namespace Riesgos.Simefin.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortafoliosController : ControllerBase
    {

        private readonly IPortfolioUseCase _portfolioUseCase;
        //private readonly IMapper _mapper;
        private ResponseDTO _response = new ResponseDTO();

        public PortafoliosController(IPortfolioUseCase portfolioUseCase, IConfiguration configuration)
        {
            _portfolioUseCase = portfolioUseCase;
            //_mapper = mapper;
        }


        /// <summary>
        /// Obtener todos los portafolios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portafolio>>> GetAllPortfolios()
        {
            var portafolios = await _portfolioUseCase.GetAllPortfolio();
            return Ok(portafolios);
        }

        /// <summary>
        /// Obtener todos los portafolios de acuerdo a una fecha
        /// </summary>
        /// <param name="fechaPosicion">Fecha de posición</param>
        /// <returns></returns>
        [HttpGet("fechaPosicion:string", Name = "GetPortafolioFecha")]
        public async Task<IActionResult> GetPortfolioByDate(string fechaPosicion)
        {

            if (fechaPosicion == null || fechaPosicion == "0")
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Se debe ingresar una fecha válida");
                return this.GetStatusResponse(_response);
            }

            if (fechaPosicion.Length < 8)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("El formato de la fecha no es válido, Se espera una fecha de la siguiente manera: yyyyMMdd ");
                return this.GetStatusResponse(_response);
            }

            _response = await _portfolioUseCase.GetPortfolioByDate(fechaPosicion);

            return this.GetStatusResponse(_response);
        }

        /// <summary>
        /// Obtener portafolio por Identificador
        /// </summary>
        /// <param name="idPortafolio"></param>
        /// <returns></returns>
        [HttpGet("{idPortafolio}")]
        public async Task<IActionResult> GetPortfolioById(int idPortafolio)
        {
            _response = await _portfolioUseCase.GetPortfolioById(idPortafolio);
            return this.GetStatusResponse(_response);
        }

        ///// <summary>
        ///// Crear nuevo Portafolio
        ///// </summary>
        ///// <param name="request">Datos del Portafolio</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult<int>> CreatePortfolio(PortafolioCreateDTO request)
        //{
        //    var id = await _portfolioUseCase.CreatePortfolio(request);
        //    return CreatedAtAction(nameof(GetPortfolioById), new { id }, id);
        //}

        /// <summary>
        /// Actualización de un portafolio
        /// </summary>
        /// <param name="idPortafolio">Identificador</param>
        /// <param name="request">Contenedor con información de Portafolio</param>
        /// <returns></returns>
        [HttpPut("{idPortafolio}")]
        public async Task<IActionResult> UpdatePortfolio(int idPortafolio, PortafolioUpdateDTO request)
        {
            if (idPortafolio == 0 || (request == null || string.IsNullOrEmpty(request.ListaDatos)))
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                this.GetStatusResponse(_response);
            }

            _response = await _portfolioUseCase.UpdatePortfolio(idPortafolio, request!);

            return this.GetStatusResponse(_response);
        }

        /// <summary>
        /// Eliminar un Portafolio
        /// </summary>
        /// <param name="idPortafolio">Identificador de Portafolio</param>
        /// <returns></returns>
        [HttpDelete("{idPortafolio}")]
        public async Task<IActionResult> DeletePortfolio(int idPortafolio)
        {
            _response = await _portfolioUseCase.DeletePortfolio(idPortafolio);
            return this.GetStatusResponse(_response);
        }

        /// <summary>
        /// Carga de archivo de excel con registros de portafolios
        /// </summary>
        /// <param name="file">Archivo de Excel</param>
        /// <returns></returns>
        [HttpPost("ExcelLoad")]
        public async Task<IActionResult> ExcelLoad(IFormFile? file)
        {
            _response = await _portfolioUseCase.ExcelLoad(file!);

            return this.GetStatusResponse(_response);
        }

        //
        [HttpGet("fileDate:string", Name = "GetFileCSV")]
        public async Task<IActionResult> GetFileCSV(string fileDate)
        {
            var fileBytes = await _portfolioUseCase.GetFileCSV(fileDate);

            //return this.GetStatusResponse(_response);

            //return await _portfolioUseCase.GetFileCSV(fileDate);
            string fileName = "dataCSV.csv";
            //return await File(fileBytes, "text/csv", fileName);

            return File(fileBytes, "text/csv", fileName);
        }

        /// <summary>
        /// Obteniendo el Status final a retornar
        /// </summary>
        /// <param name="response">Respuesta del servicio</param>
        /// <returns></returns>
        private IActionResult GetStatusResponse(ResponseDTO response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                default:
                    return Ok();
            }
        }


    }

}
