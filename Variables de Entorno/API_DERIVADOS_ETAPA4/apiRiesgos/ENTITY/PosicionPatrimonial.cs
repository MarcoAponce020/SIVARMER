namespace ENTITY
{
    public class PosicionPatrimonial
    {
        public int FechaValuacion { get; set; }
        public int FechaInicioOper { get; set; }
        public long NumOpera { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string TipoInstrumento { get; set; } = string.Empty;
        public int NumTitulosPos { get; set; }
        public decimal Monto { get; set; }
        public int Plazo { get; set; }
        public int DT { get; set; }
        public int DXV { get; set; }
        public decimal Tasa { get; set; }
        public decimal PremioDev { get; set; }
        public decimal ImpCIntereses { get; set; }
        public int FechaVencimiento { get; set; }
    }
}
