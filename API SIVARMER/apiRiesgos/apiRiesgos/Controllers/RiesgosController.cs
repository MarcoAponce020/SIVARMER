using apiRiesgos.Servicios;
using DAL;
using ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Reflection;

namespace apiRiesgos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiesgosController : Controller
    {
        private readonly IServicio_API _servicioApi;
        private readonly ILogger<RiesgosController> _logger;
        private readonly IConfiguration _config;
        IConfiguration configuration;
        GetOracleConnection getOracleConnection;
        OracleConnection conn;
        Respuesta respuesta1 = new Respuesta();

        public RiesgosController(IServicio_API servicioApi, ILogger<RiesgosController> logger, IConfiguration _configuration)
        {
            _servicioApi = servicioApi;
            _logger = logger;
            try
            {
                configuration = _configuration;
                getOracleConnection = new GetOracleConnection(configuration);

                conn = getOracleConnection.GetConnection("Connection");
            }
            catch (Exception ex)
            {
                respuesta1.mensaje = ex.Message;
            }


            //string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        }
        [HttpGet("{tipoReporte}/{fechaConsulta}/{token}")]
        public async Task<Respuesta> GetRiesgos(int tipoReporte, int fechaConsulta, string token)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            ResultadoAPI respuestaAPI = new ResultadoAPI();
            
            if (tipoReporte == 1)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                    respuestaAPI.mensaje = respuesta1.mensaje;
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaValuacionReportos != null)
                {
                    respuesta = new DALRiesgos().guardarValuacionReportos(respuestaAPI.listaValuacionReportos, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 2)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaTenenciaTitulos != null)
                {
                    respuesta = new DALRiesgos().guardarTenenciaTitulos(respuestaAPI.listaTenenciaTitulos, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 3)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaComprasMesaDinero != null)
                {
                    respuesta = new DALRiesgos().guardarComprasMesaDinero(respuestaAPI.listaComprasMesaDinero, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 4)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaComprasTesoreria != null)
                {
                    respuesta = new DALRiesgos().guardarComprasTesoreria(respuestaAPI.listaComprasTesoreria, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 5)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaPosicionPatrimonial != null)
                {
                    respuesta = new DALRiesgos().guardarPosicionPatrimonial(respuestaAPI.listaPosicionPatrimonial, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 7)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaReporteREVAME != null)
                {
                    respuesta = new DALRiesgos().guardarReporteREVAME(respuestaAPI.listaReporteREVAME, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 8)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaPosicionCalculoVAR != null)
                {
                    respuesta = new DALRiesgos().guardarPosicionCalculoVAR(respuestaAPI.listaPosicionCalculoVAR, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 9)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaPosicionRegulatorios != null)
                {
                    respuesta = new DALRiesgos().guardarPosicionRegulatorios(respuestaAPI.listaPosicionRegulatorios, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 10)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaReportePosicionTesoreria != null)
                {
                    respuesta = new DALRiesgos().guardarReportePosicionTesoreria(respuestaAPI.listaReportePosicionTesoreria, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 11)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaPosicionGlobalTitulos != null)
                {
                    respuesta = new DALRiesgos().guardarGlobalTitulos(respuestaAPI.listaPosicionGlobalTitulos, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 12)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaMovimientosTesoreria != null)
                {
                    respuesta = new DALRiesgos().guardarMovimientosTesoreria(respuestaAPI.listaMovimientosTesoreria, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 14)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }
                if (respuestaAPI.listaFlujosSwaps != null)
                {
                    respuesta = new DALRiesgos().guardarFlujosSwaps(respuestaAPI.listaFlujosSwaps, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 16)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }
                if (respuestaAPI.listaCaracteristicasSwaps != null)
                {
                    respuesta = new DALRiesgos().guardarCaracteristicasSwaps(respuestaAPI.listaCaracteristicasSwaps, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 15)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }
                if (respuestaAPI.listaFlujosPosicionesPrimarias != null)
                {
                    respuesta = new DALRiesgos().guardarFlujosPosicionesPrimarias(respuestaAPI.listaFlujosPosicionesPrimarias, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else if (tipoReporte == 20)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }
                if (respuestaAPI.listaReporteMDCambios != null)
                {
                    respuesta = new DALRiesgos().guardarReporteMDCambios(respuestaAPI.listaReporteMDCambios, conn);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else
            {
                respuesta.mensaje = "El número de reporte es invalido.";
            }

            return respuesta;
        }
        [HttpGet("{tipoReporte}/{fechaIni}/{fechaFin}/{token}")]
        public async Task<Respuesta> GetRiesgosRango(int tipoReporte, int fechaIni, int fechaFin, string token)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoAPI respuestaAPI = new ResultadoAPI();
            if (tipoReporte == 6)
            {
                try
                {
                    respuestaAPI = await _servicioApi.GetRiesgosRango(tipoReporte, fechaIni, fechaFin, token);
                }
                catch (Exception ex)
                {
                    respuesta.mensaje = "Error: " + ex.Message + "--" + respuestaAPI.mensaje;
                }

                if (respuestaAPI.listaComprasVentasOperador != null)
                {
                    respuesta = new DALRiesgos().guardarComprasVentasOperador(respuestaAPI.listaComprasVentasOperador, conn, fechaIni, fechaFin);
                }
                else
                {
                    respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
                }
            }
            else
            {
                respuesta.mensaje = "El número de reporte es invalido.";
            }

            return respuesta;
        }
    }
}
