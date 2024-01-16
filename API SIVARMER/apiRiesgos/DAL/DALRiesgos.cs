using DAO;
using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALRiesgos
    {
        public Respuesta guardarValuacionReportos(List<ValuacionReportos> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarValuacionReportos(reporte, con);

            return resp;
        }
        public Respuesta guardarTenenciaTitulos(List<TenenciaTitulos> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarTenenciaTitulos(reporte, con);

            return resp;
        }
        public Respuesta guardarComprasMesaDinero(List<ComprasMesaDinero> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarComprasMesaDinero(reporte, con);

            return resp;
        }
        public Respuesta guardarComprasTesoreria(List<ComprasTesoreria> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarComprasTesoreria(reporte, con);

            return resp;
        }
        public Respuesta guardarPosicionPatrimonial(List<PosicionPatrimonial> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionPatrimonial(reporte, con);

            return resp;
        }
        public Respuesta guardarReporteREVAME(List<ReporteREVAME> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarReporteREVAME(reporte, con);

            return resp;
        }
        public Respuesta guardarPosicionCalculoVAR(List<PosicionCalculoVAR> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionCalculoVAR(reporte, conexionRiesgos);

            return resp;
        }
        public Respuesta guardarPosicionRegulatorios(List<PosicionRegulatorios> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionRegulatorios(reporte, conexionRiesgos);

            return resp;
        }
        public Respuesta guardarReportePosicionTesoreria(List<ReportePosicionTesoreria> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarReportePosicionTesoreria(reporte, conexionRiesgos);

            return resp;
        }
        public Respuesta guardarGlobalTitulos(List<PosicionGlobalTitulos> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionGlobalTitulos(reporte, con);

            return resp;
        }
        public Respuesta guardarMovimientosTesoreria(List<MovimientosTesoreria> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarMovimientosTesoreria(reporte, conexionRiesgos);

            return resp;
        }
        public Respuesta guardarComprasVentasOperador(List<ComprasVentasOperador> reporte, OracleConnection con, int fechaIni, int fechaFin)
        {
            Respuesta resp = new DAORiesgos().guardarComprasVentasOperador(reporte, con, fechaIni, fechaFin);

            return resp;
        }
        public Respuesta guardarReporteMDCambios(List<ReporteMDCambios> reporte, OracleConnection conexionRiesgos)
        {
            Respuesta resp = new DAORiesgos().guardarReporteMDCambios(reporte, conexionRiesgos);

            return resp;
        }
        public Respuesta guardarFlujosSwaps(List<FlujosSwaps> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarFlujosSwaps(reporte, con);

            return resp;
        }
        public Respuesta guardarCaracteristicasSwaps(List<CaracteristicasSwaps> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarCaracteristicasSwaps(reporte, con);

            return resp;
        }
        public Respuesta guardarPosicionPrimariaSwaps(List<PosicionPrimariaSwaps> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarPosicionPrimariaSwaps(reporte, con);

            return resp;
        }
        public Respuesta guardarFlujosPosicionesPrimarias(List<FlujosPosicionesPrimarias> reporte, OracleConnection con)
        {
            Respuesta resp = new DAORiesgos().guardarFlujosPosicionesPrimarias(reporte, con);

            return resp;
        }

    }
}
