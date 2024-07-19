namespace ENTITY
{
    public class ReporteOperacionCVDivisas
    {
        public int Fecha { get; set; }
        public int Cve_contraparte { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Tipo_oper { get; set; }
        public string Posicion { get; set; } = string.Empty;
        public long Monto { get; set; }
        public string Tipo_moneda { get; set; } = string.Empty;
        public int Cve_moneda { get; set; }
        public decimal Tipo_cambio_conc { get; set; }
        public decimal Tipo_cambio_mdo { get; set; }

    }
}
