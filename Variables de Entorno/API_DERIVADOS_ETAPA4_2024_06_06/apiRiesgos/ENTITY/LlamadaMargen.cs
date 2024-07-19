namespace ENTITY
{
    public class LlamadaMargen
    {
        public string CveContPP { get; set; } = string.Empty;
        public string NombreContra { get; set; } = string.Empty;
        public decimal Thresh_C { get; set; }
        public decimal MMT_C { get; set; }
        public decimal Thresh_B { get; set; }
        public decimal MMT_B { get; set; }
        public string Mon_Calc { get; set; } = string.Empty;
        public decimal Val_Simefin { get; set; }
        public decimal Val_Agente { get; set; }
        public decimal Exposicion_Neta { get; set; }
     
        public decimal Val_Gtias { get; set; }
        public decimal Gtias_Programadas { get; set; }
        public decimal Llamada_Margen { get; set; }
    }
}
