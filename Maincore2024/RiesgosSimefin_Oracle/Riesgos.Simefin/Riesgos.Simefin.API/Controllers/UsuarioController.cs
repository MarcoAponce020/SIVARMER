using Microsoft.AspNetCore.Mvc;
using Riesgos.Simefin.Application.DTOs;
using Riesgos.Simefin.Application.DTOs.User;
using Riesgos.Simefin.Application.Interfaces.Authorization;
using Riesgos.Simefin.Application.Interfaces.User;
using System.Net;

namespace Riesgos.Simefin.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUserUseCase _userUseCase;
        private readonly IAuthorizationUseCase _authorizationUseCase;

        public UsuarioController(IUserUseCase userUseCase, IAuthorizationUseCase authorizationUseCase)
        {
            _userUseCase = userUseCase;
            _authorizationUseCase = authorizationUseCase;
        }
        

        /// <summary>
        /// Obtener AccessToken a partir de las credenciales de usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            if (!ModelState.IsValid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsExitoso = false;
                response.ErrorMessages.Add("Username o Password son requeridos.");
                return BadRequest(response);
            }

            response = await _userUseCase.Login(request);
            return this.GetStatusResponse(response);
        }

        /// <summary>
        /// Generar nuevo AccessToken a partir de un RefreshToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] TokenDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            if (!ModelState.IsValid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsExitoso = false;
                response.ErrorMessages.Add("AccessToken o RefreshToken son requeridos.");
                return BadRequest(response);
            }

            response = await _authorizationUseCase.GenerateRefreshAccessToken(request);

            return this.GetStatusResponse(response);
        }

        /// <summary>
        /// Registrar nuevo usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO request)
        {
            ResponseDTO response = new ResponseDTO();

            if (!ModelState.IsValid)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add("Todos los datos son requeridos.");
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            response = await _userUseCase.Register(request);

            return this.GetStatusResponse(response);
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
