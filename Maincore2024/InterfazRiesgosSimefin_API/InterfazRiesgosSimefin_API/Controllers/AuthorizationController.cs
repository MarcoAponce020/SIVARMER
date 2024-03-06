using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using InterfazRiesgosSimefin_API.Repository.User;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InterfazRiesgosSimefin_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _ctx;

        public AuthorizationController(ApplicationDbContext ctx, IAuthorizationService authorizationService)
        {
            _ctx = ctx;
            _authorizationService = authorizationService;
        }


        [HttpPost]
        [Route("login")]
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

        //[Authorize]
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

        //[Authorize]
        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            APIResponse response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add("Todos los datos son requeridos.");
                response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            response = await _authorizationService.Register(modelo);

            return Ok(response);
        }


    }

}
