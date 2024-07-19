namespace ENTITY
{
    public class PosicionGlobalTitulos
    {
        public int Fecha { get; set; }
        public string Emision { get; set; } = string.Empty;
        public string TipoInventario { get; set; } = string.Empty;
        public long TotalTitulos { get; set; }
        public long TitulosRepIntermediarios { get; set; }
        public long TitulosGarOtorgados { get; set; }
        public long TenenciaIndeval { get; set; }
        public long TitulosReportoCli { get; set; }
        public long TitulosEnAdmon { get; set; }
        public long TitulosDisponibles { get; set; }
        public long TitulosGarRecibidos { get; set; }
        public long TitulosRecibidosCustodia { get; set; }
    }
}
