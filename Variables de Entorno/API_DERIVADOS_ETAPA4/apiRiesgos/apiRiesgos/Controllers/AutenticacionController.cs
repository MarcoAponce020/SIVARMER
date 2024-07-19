using apiRiesgos.Servicios;
using ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace apiRiesgos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IServicioApi _servicioApi;

        public AutenticacionController(IServicioApi servicioApi)
        {
            _servicioApi = servicioApi;
        }

        [HttpGet("{client_id}/{client_secret}")]
        public async Task<ResultadoCredencial> Autenticacion(string client_id, string client_secret)
        {
            var resultado = await _servicioApi.Autenticacion(client_id, client_secret);

            return resultado;
        }
    }
}
