namespace ENTITY
{
    public class ReporteRevame
    {
        public string Fecha { get; set; } = string.Empty;
        public string Portafolio { get; set; } = string.Empty;
        public string Emision { get; set; } = string.Empty;
        public long Titulos { get; set; }
        public decimal PrecioLimpio { get; set; }
        public decimal PrecioSucio { get; set; }
        public decimal ImporteLimpio { get; set; }
        public decimal ImporteSucio { get; set; }
        public decimal PrecioLimpioLib { get; set; }
        public decimal PrecioSucioLib { get; set; }
        public decimal ImporteLimpioLib { get; set; }
        public decimal ImporteSucioLib { get; set; }
        public decimal Valuacion { get; set; }
        public decimal PrecioMercado { get; set; }
    }
}
