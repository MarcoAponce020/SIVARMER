namespace ENTITY
{
    public class ResultadoApi
    {


        public string? mensaje { get; set; } = string.Empty;
        public List<ValuacionReportos>? ListaValuacionReportos { get; set; }
        public List<TenenciaTitulos>? ListaTenenciaTitulos { get; set; }
        public List<ComprasMesaDinero>? ListaComprasMesaDinero { get; set; }
        public List<ComprasTesoreria>? ListaComprasTesoreria { get; set; }
        public List<PosicionPatrimonial>? ListaPosicionPatrimonial { get; set; }
        public List<ReporteRevame>? ListaReporteREVAME { get; set; }
        public List<PosicionCalculoVar>? ListaPosicionCalculoVAR { get; set; }
        public List<PosicionRegulatorios>? ListaPosicionRegulatorios { get; set; }
        public List<ReportePosicionTesoreria>? ListaReportePosicionTesoreria { get; set; }
        public List<PosicionGlobalTitulos>? ListaPosicionGlobalTitulos { get; set; }
        public List<MovimientosTesoreria>? ListaMovimientosTesoreria { get; set; }
        public List<PosicionForwards>? ListaPosicionForwards { get; set; }
        public List<FlujosSwaps>? ListaFlujosSwaps { get; set; }
        public List<FlujosPosicionesPrimarias>? ListaFlujosPosicionesPrimarias { get; set; }
        public List<CaracteristicasSwaps>? ListaCaracteristicasSwaps { get; set; }
        public List<LlamadaMargen>? ListaLlamadaMargen { get; set; }
        public List<PosicionPrimariaSwaps>? ListaPosicionPrimariaSwaps { get; set; }
        public List<ReporteOperacionCVDivisas>? ListaOperacionCVDivisas { get; set; }
        public List<ComprasVentasOperador>? ListaComprasVentasOperador { get; set; }
        public List<PosicionesPrimForwards>? ListaPosicionesPrimForwards { get; set; }

    }
}
