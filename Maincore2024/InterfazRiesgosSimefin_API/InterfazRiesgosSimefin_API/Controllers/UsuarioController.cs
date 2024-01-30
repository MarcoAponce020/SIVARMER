using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using InterfazRiesgosSimefin_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace InterfazRiesgosSimefin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private APIResponse _response;
        public UsuarioController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _response = new();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO modelo)
        {
            var tokenDTO = await _usuarioRepo.Login(modelo);
            if (tokenDTO == null || string.IsNullOrEmpty(tokenDTO.AccessToken))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Username o Password son Incorrectos");
                return BadRequest(_response);
            }
            _response.IsExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Resultado = tokenDTO;
            _response.Mensaje = "Credenciales correctas";
            return Ok(_response);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            bool isUsuarioUnico = _usuarioRepo.IsUsuarioUnico(modelo.UserName);

            if (isUsuarioUnico)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("El Usuario ya Existe");
                return BadRequest(_response);
            }
            var usuario = await _usuarioRepo.Registrar(modelo);
            if (usuario == null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Error al registrar Usuario");
                return BadRequest(_response);
            }
            _response.statusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;

            return Ok(_response);
        }


        //Metodo que genera el token con un refresh token

        [HttpPost("refresh")]
        public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] TokenDTO tokenDTO)
        {
            if (ModelState.IsValid)
            {
                var tokenDTOResponse = await _usuarioRepo.RefreshAccessToken(tokenDTO);
                if (tokenDTOResponse == null || string.IsNullOrEmpty(tokenDTOResponse.AccessToken))
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    _response.ErrorMessages.Add("Token Inválido");
                    return BadRequest(_response);
                }
                 _response.statusCode = HttpStatusCode.OK;
                _response.IsExitoso = true;
                _response.Resultado = tokenDTOResponse;

                return Ok(_response);

            }
            else
            {
                _response.IsExitoso = false;
                _response.Resultado = "Entrada No válida";
                return BadRequest(_response);


            }

        }

    }
}
