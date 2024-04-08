using apiRiesgos.Servicios;
using DAL;
using ENTITY;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace apiRiesgos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiesgosController : Controller
    {
        private readonly IServicio_API _servicioApi;
        private readonly ILogger<RiesgosController> _logger;
        //private readonly IConfiguration _config;
        IConfiguration configuration;
        GetOracleConnection getOracleConnection;
        OracleConnection conn;
        OracleConnection connDerivados;
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
                connDerivados = getOracleConnection.GetConnectionDerivados("ConnectionDerivados");
            }
            catch (Exception ex)
            {
                respuesta1.mensaje = ex.Message;
            }
        }


        [HttpGet("{tipoReporte}/{fechaConsulta}/{token}")]
        public async Task<Respuesta> GetRiesgos(int tipoReporte, int fechaConsulta, string token)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.exito = false;
            respuesta.mensaje = "No hay registros en SIMEFIN para insertar en la base de datos.";
            ResultadoAPI respuestaAPI = new ResultadoAPI();

            try
            {
                respuestaAPI = await _servicioApi.GetRiesgos(tipoReporte, fechaConsulta, token);
                var catalogo_divisas_R = new DALRiesgos().GetCatalogoDivisas("RIESVARM", conn);
                var catalogo_divisas_D = new DALRiesgos().GetCatalogoDivisas("XYZWIN", connDerivados);
                var catalogo_posiciones = new DALRiesgos().GetCatalogoPosiciones(conn);

                //Si devuelve un texto, significa que hubo error
                if (!string.IsNullOrEmpty(respuestaAPI.mensaje))
                {
                    respuesta.mensaje = respuestaAPI.mensaje;
                }
                else
                {

                    switch (tipoReporte)
                    {
                        case 1:
                            if (respuestaAPI.ListaValuacionReportos != null)
                            {
                                respuesta = new DALRiesgos().guardarValuacionReportos(respuestaAPI.ListaValuacionReportos, conn);
                            }
                            break;
                        case 2:
                            if (respuestaAPI.ListaTenenciaTitulos != null)
                            {
                                respuesta = new DALRiesgos().guardarTenenciaTitulos(respuestaAPI.ListaTenenciaTitulos, conn);
                            }
                            break;
                        case 3:
                            if (respuestaAPI.ListaComprasMesaDinero != null)
                            {
                                respuesta = new DALRiesgos().guardarComprasMesaDinero(respuestaAPI.ListaComprasMesaDinero, conn);
                            }
                            break;
                        case 4:
                            if (respuestaAPI.ListaComprasTesoreria != null)
                            {
                                respuesta = new DALRiesgos().guardarComprasTesoreria(respuestaAPI.ListaComprasTesoreria, conn);
                            }
                            break;
                        case 5:
                            if (respuestaAPI.ListaPosicionPatrimonial != null)
                            {
                                respuesta = new DALRiesgos().guardarPosicionPatrimonial(respuestaAPI.ListaPosicionPatrimonial, conn);
                            }
                            break;
                        case 6:
                            break;
                        case 7:
                            if (respuestaAPI.ListaReporteREVAME != null)
                            {
                                respuesta = new DALRiesgos().guardarReporteREVAME(respuestaAPI.ListaReporteREVAME, conn);
                            }
                            break;
                        case 8:
                            if (respuestaAPI.ListaPosicionCalculoVAR != null)
                            {
                                respuesta = new DALRiesgos().guardarPosicionCalculoVAR(respuestaAPI.ListaPosicionCalculoVAR, conn);
                            }
                            break;
                        case 9:
                            if (respuestaAPI.ListaPosicionRegulatorios != null)
                            {
                                respuesta = new DALRiesgos().guardarPosicionRegulatorios(respuestaAPI.ListaPosicionRegulatorios, conn);
                            }
                            break;
                        case 10:
                            if (respuestaAPI.ListaReportePosicionTesoreria != null)
                            {
                                respuesta = new DALRiesgos().guardarReportePosicionTesoreria(respuestaAPI.ListaReportePosicionTesoreria, conn);
                            }
                            break;
                        case 11:
                            if (respuestaAPI.ListaPosicionGlobalTitulos != null)
                            {
                                respuesta = new DALRiesgos().guardarGlobalTitulos(respuestaAPI.ListaPosicionGlobalTitulos, conn);
                            }
                            break;
                        case 12:
                            if (respuestaAPI.ListaMovimientosTesoreria != null)
                            {
                                respuesta = new DALRiesgos().guardarMovimientosTesoreria(respuestaAPI.ListaMovimientosTesoreria, conn);
                            }
                            break;
                        case 13:
                            if (respuestaAPI.ListaPosicionForwards != null)
                            {
                                respuesta = new DALRiesgos().guardarPosicionForwards(respuestaAPI.ListaPosicionForwards, connDerivados);
                            }
                            break;
                        case 14:
                            if (respuestaAPI.ListaFlujosSwaps != null)
                            {
                                respuesta = new DALRiesgos().guardarFlujosSwaps(respuestaAPI.ListaFlujosSwaps, catalogo_divisas_D, connDerivados);
                            }
                            break;
                        case 15:
                            if (respuestaAPI.ListaFlujosPosicionesPrimarias != null && respuestaAPI.ListaFlujosPosicionesPrimarias.Count > 0)
                            {
                                respuesta = await new DALRiesgos().FlujosPosicionesPrimariasProcess(fechaConsulta, respuestaAPI.ListaFlujosPosicionesPrimarias, conn);
                            }
                            break;
                        case 16:
                            if (respuestaAPI.ListaCaracteristicasSwaps != null)
                            {
                                respuesta = new DALRiesgos().guardarCaracteristicasSwaps(respuestaAPI.ListaCaracteristicasSwaps, catalogo_divisas_D, connDerivados);
                            }
                            break;
                        case 17:
                            if (respuestaAPI.ListaLlamadaMargen != null)
                            {
                                respuesta = new DALRiesgos().guardarLlamadaMargen(respuestaAPI.ListaLlamadaMargen, connDerivados);
                            }
                            break;
                        case 18:
                            if (respuestaAPI.ListaPosicionPrimariaSwaps != null && respuestaAPI.ListaPosicionPrimariaSwaps.Count > 0)
                            {
                                respuesta = await new DALRiesgos().guardarPosicionPrimariaSwaps(fechaConsulta, respuestaAPI.ListaPosicionPrimariaSwaps, conn);
                            }
                            break;
                        case 19:
                            if (respuestaAPI.ListaOperacionCVDivisas != null)
                            {
                                respuesta = new DALRiesgos().guardarReporteOperacionCVDivisas(respuestaAPI.ListaOperacionCVDivisas, catalogo_divisas_R, catalogo_posiciones, conn);
                            }
                            break;
                        case 20:
                            if (respuestaAPI.ListaPosicionesPrimForwards != null)
                            {
                                respuesta = new DALRiesgos().guardarPosicionesPrimForwards(respuestaAPI.ListaPosicionesPrimForwards, catalogo_divisas_R, conn);
                            }
                            break;
                        default:
                            respuesta.mensaje = "No fué posible encontrar el número de reporte.";
                            break;
                    } //switch
                } //if

            } //try
            catch (Exception ex)
            {
                respuesta.mensaje = $"Error: {ex.Message} -- {respuestaAPI.mensaje}.";
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

                if (respuestaAPI.ListaComprasVentasOperador != null)
                {
                    respuesta = new DALRiesgos().guardarComprasVentasOperador(respuestaAPI.ListaComprasVentasOperador, conn, fechaIni, fechaFin);
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
