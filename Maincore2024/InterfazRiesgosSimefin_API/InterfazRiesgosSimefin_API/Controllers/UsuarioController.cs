using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AuthorizationService = InterfazRiesgosSimefin_API.Services.Authorization;

namespace InterfazRiesgosSimefin_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly AuthorizationService.IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _ctx;

        public UsuarioController(ApplicationDbContext ctx, AuthorizationService.IAuthorizationService authorizationService)
        {
            _ctx = ctx;
            _authorizationService = authorizationService;
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
            APIResponse response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.statusCode = HttpStatusCode.BadRequest;
                response.IsExitoso = false;
                response.ErrorMessages.Add("Username o Password son requeridos.");
                return BadRequest(response);
            }

            response = await _authorizationService.Login(request);
            return Ok(response);
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
            APIResponse response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.statusCode = HttpStatusCode.BadRequest;
                response.IsExitoso = false;
                response.ErrorMessages.Add("AccessToken o RefreshToken son requeridos.");
                return BadRequest(response);
            }

            response = await _authorizationService.GenerateRefreshAccessToken(request);

            return Ok(response);
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
            APIResponse response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add("Todos los datos son requeridos.");
                response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            response = await _authorizationService.Register(request);

            return Ok(response);
        }


    }

}
