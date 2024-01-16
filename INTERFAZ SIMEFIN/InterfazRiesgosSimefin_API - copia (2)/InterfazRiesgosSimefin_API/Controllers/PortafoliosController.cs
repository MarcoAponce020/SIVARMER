using AutoMapper;
using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using InterfazRiesgosSimefin_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace InterfazRiesgosSimefin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortafoliosController : ControllerBase
    {


        private readonly ILogger<PortafoliosController> _logger;
        private readonly IPortafolioRepository _portafolioRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        public PortafoliosController(ILogger<PortafoliosController> logger, IPortafolioRepository portafolioRepo, IMapper mapper)
        {
            _logger = logger;
            _portafolioRepo = portafolioRepo;
            _mapper = mapper;
            _response = new();
        }

        /*Metodo para obtener los portafolios*/
        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPortafolios()
        {
            try
            {
                _logger.LogInformation("Obtener los Portafolios");
                IEnumerable<Portafolio> portafolioList = await _portafolioRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<PortafolioDto>>(portafolioList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        /*retorna un solo objeto*/
        [HttpGet("{idPortafolio:int}", Name = "GetPortafolio")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetPortafolio(int idPortafolio)
        {
            try
            {
                if (idPortafolio == 0)
                {
                    _logger.LogError("Error al traer el portafolio con Id:  " + idPortafolio);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var portafolio = PortafolioStore.portafolioList.FirstOrDefault(p => p.IdPortafolio == idPortafolio);
                var portafolio = await _portafolioRepo.Obtener(p => p.IdPortafolio == idPortafolio);

                if (portafolio == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<PortafolioDto>(portafolio);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }


        [HttpGet("fechaPosicion:string", Name = "GetPortafolioFecha")] //retorna un solo objeto por fecha
        [Authorize(Roles = "admin,developer", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetPortafolioFecha(string fechaPosicion)
        {

            try
            {
                string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;


                if (fechaPosicion == null || fechaPosicion == "0")
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("Se debe ingresar una fecha válida");
                    return BadRequest(_response);
                }
                if (fechaPosicion.Length < 8)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("El formato de la fecha no es válido, Se espera una fecha de la siguiente manera: yyyyMMdd ");
                    return BadRequest(_response);
                }

                IEnumerable<Portafolio> portafolioList = await _portafolioRepo.ObtenerTodos(p => p.F_Posicion == fechaPosicion);
                
                if (portafolioList.Count() == 0)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("No existe Información para esta Fecha");
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<IEnumerable<PortafolioDto>>(portafolioList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return Ok(_response);

        }



        /*Crear un portafolio*/
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CrearPortafolio([FromForm] PortafolioCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                /*if (await _portafolioRepo.Obtener(p => p.IdPortafolio == createDto.F_Posicion) != null)
                {
                    ModelState.AddModelError("SubPortafolioExistente", "¡El subportafolio  ya existe!");
                    return BadRequest(ModelState);
                }*/
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Portafolio modelo = _mapper.Map<Portafolio>(createDto);

                await _portafolioRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPortafolio", new { id = modelo.IdPortafolio }, modelo);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        /*Eliminar un portafolio*/
        [HttpDelete("{idPortafolio:int}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePortafolio(int idPortafolio)
        {
            try
            {
                if (idPortafolio == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var deleteportafolio = await _portafolioRepo.Obtener(p => p.IdPortafolio == idPortafolio);

                if (deleteportafolio == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _portafolioRepo.Remover(deleteportafolio);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{idPortafolio:int}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePortafolio(int idPortafolio, [FromBody] PortafolioUpdateDto updateDto)
        {

            if (updateDto == null || idPortafolio != updateDto.IdPortafolio)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest();

            }

            Portafolio modelo = _mapper.Map<Portafolio>(updateDto);

            await _portafolioRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }

       

    }
}
