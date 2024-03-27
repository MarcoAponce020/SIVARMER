using apiRiesgos.Servicios;
using ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiRiesgos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IServicio_API _servicioApi;

        public AutenticacionController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }
        [HttpGet("{client_id}/{client_secret}")]
        public async Task<ResultadoCredencial> Autenticacion(string client_id, string client_secret)
        {
            ResultadoCredencial resultado = new ResultadoCredencial();
            resultado.exito = false;
            if (_servicioApi.Conexion().Result.exito)
            {
                resultado = await _servicioApi.Autenticacion(client_id, client_secret);
            }
            else
            {
                resultado.mensaje = "Hubo un error al conectar con la API";
            }


            return resultado;
        }
    }
}
