using DAO;
using ENTITY;
using Oracle.ManagedDataAccess.Client;

namespace DAL
{
    public class DALRiesgos
    {

        /// <summary>
        /// Reporte # 1
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarValuacionReportos(List<ValuacionReportos> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarValuacionReportos(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 2
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarTenenciaTitulos(List<TenenciaTitulos> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarTenenciaTitulos(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 3
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarComprasMesaDinero(List<ComprasMesaDinero> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarComprasMesaDinero(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 4
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarComprasTesoreria(List<ComprasTesoreria> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarComprasTesoreria(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 5
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarPosicionPatrimonial(List<PosicionPatrimonial> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionPatrimonial(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 7
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarReporteREVAME(List<ReporteRevame> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarReporteREVAME(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 8
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarPosicionCalculoVAR(List<PosicionCalculoVar> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionCalculoVAR(reporte, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte # 9
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarPosicionRegulatorios(List<PosicionRegulatorios> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionRegulatorios(reporte, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte # 10
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarReportePosicionTesoreria(List<ReportePosicionTesoreria> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarReportePosicionTesoreria(reporte, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte # 11
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarGlobalTitulos(List<PosicionGlobalTitulos> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionGlobalTitulos(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 12
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarMovimientosTesoreria(List<MovimientosTesoreria> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarMovimientosTesoreria(reporte, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte # 13
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarPosicionForwards(List<PosicionForwards> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionForwards(reporte, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte # 14
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarFlujosSwaps(List<FlujosSwaps> reporte, List<CatalogoDivisas> catalogoDivisas, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarFlujosSwaps(reporte, catalogoDivisas, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 15
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public async Task<Respuesta> FlujosPosicionesPrimariasProcess(int fechaConsulta, List<FlujosPosicionesPrimarias> reporte, OracleConnection con)
        {
            Respuesta resp = await new DAORiesgos().FlujosPosicionesPrimariasProcess(fechaConsulta, reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 16
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarCaracteristicasSwaps(List<CaracteristicasSwaps> reporte, List<CatalogoDivisas> catalogoDivisas, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarCaracteristicasSwaps(reporte, catalogoDivisas, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 17
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public Respuesta guardarLlamadaMargen(List<LlamadaMargen> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarLlamadaMargen(reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte # 18
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public async Task<Respuesta> guardarPosicionPrimariaSwaps(int fechaConsulta, List<PosicionPrimariaSwaps> reporte, OracleConnection con)
        {
            Respuesta resp = await new DAORiesgos().guardarPosicionPrimariaSwaps(fechaConsulta, reporte, con);

            return resp;
        }

        /// <summary>
        /// Reporte No. 19
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarReporteOperacionCVDivisas(List<ReporteOperacionCVDivisas> reporte, List<CatalogoDivisas> catalogoDivisas, List<CatalogoPosiciones> catalogoPosiciones, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarReporteOperacionCVDivisas(reporte, catalogoDivisas, catalogoPosiciones, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte # 20
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="conexionRiesgos"></param>
        /// <returns></returns>
        public Respuesta guardarPosicionesPrimForwards(List<PosicionesPrimForwards> reporte, List<CatalogoDivisas> catalogoDivisas, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionesPrimForwards(reporte, catalogoDivisas, conexionRiesgos);

            return resp;
        }

        /// <summary>
        /// Reporte de Riesgo-Rango
        /// </summary>
        /// <param name="reporte"></param>
        /// <param name="con"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public Respuesta guardarComprasVentasOperador(List<ComprasVentasOperador> reporte, OracleConnection con, int fechaIni, int fechaFin)
        {
            Respuesta resp = new DAORiesgos().guardarComprasVentasOperador(reporte, con, fechaIni, fechaFin);

            return resp;
        }

        public List<CatalogoDivisas> GetCatalogoDivisas(string esquema, OracleConnection conexionRiesgos)
        {
            var response = new DAORiesgos().GetCatalogoDivisas(esquema, conexionRiesgos);
            return response;
        }

        public List<CatalogoPosiciones> GetCatalogoPosiciones(OracleConnection conexionRiesgos)
        {
            var response = new DAORiesgos().GetCatalogoPosiciones(conexionRiesgos);
            return response;
        }

    }
}
