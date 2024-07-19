namespace ENTITY
{
    public class ReportePosicionTesoreria
    {
        public string Emision { get; set; } = string.Empty;

        public string Posicion { get; set; } = string.Empty;
        public string Operacion { get; set; } = string.Empty;
        public int FechaCorte { get; set; }
        public long Titulos { get; set; }
        public decimal PrecioMercado { get; set; }
        public int FechaVto { get; set; }
        public decimal TasaMercado { get; set; }
        public decimal PrecioLibros { get; set; }
        public decimal TasaCupon { get; set; }
        public decimal ValorNominal { get; set; }
        public int FechaEmision { get; set; }
        public int FechaVtoCup { get; set; }
        public int ClaveInstrumento { get; set; }
        public string ClaveEmisor { get; set; } = string.Empty;
        ////public decimal Duracion { get; set; }
        public int Duracion { get; set; }
        public string TipoTasa { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Mercado { get; set; } = string.Empty;
        public string Contrato { get; set; } = string.Empty;
        public int PlazoCupon { get; set; }
        public int PlazoRepo { get; set; }
        public bool Udizado { get; set; }
    }
}
