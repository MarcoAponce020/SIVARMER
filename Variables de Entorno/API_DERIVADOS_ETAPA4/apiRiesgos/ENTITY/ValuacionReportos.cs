namespace ENTITY
{
    public class ValuacionReportos
    {
        public int FechaValuacion { get; set; }
        public string Emision { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Fecha { get; set; }
        public string NumContrato { get; set; } = string.Empty;
        public string NumReferencia { get; set; } = string.Empty;
        public string MovimientoOper { get; set; } = string.Empty;
        public string Parte { get; set; } = string.Empty;
        public int Titulos { get; set; }
        public decimal PrecioOperacion { get; set; }
        public decimal ImporteOperacion { get; set; }
        public decimal ImporteLibros { get; set; }
        public int DiasTranscurridos { get; set; }
        public int DxV { get; set; }
        public decimal TasaVencimiento { get; set; }
        public decimal TasaDiaria { get; set; }
        public decimal TasaCurva { get; set; }
        public decimal Premio { get; set; }
        public decimal PrecioVector { get; set; }
        public decimal ValorMercado { get; set; }
        public int Portafolio { get; set; }
    }
}
