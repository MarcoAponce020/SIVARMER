namespace ENTITY
{
    public class PosicionForwards
    {
        public int F_Posicion { get; set; }
        public string Clave_Op { get; set; } = string.Empty;
        public string T_Operacion { get; set; } = string.Empty;
        public decimal M_Nocional { get; set; }
        public int F_inicio { get; set; }
        public int F_Vencimiento { get; set; }
        public int F_Liquidacion { get; set; }
        public string Clave_Producto { get; set; } = string.Empty;
        public int Plazo_Fwd { get; set; }
        public decimal Tc_Pactado { get; set; }
        public string Intencion { get; set; } = string.Empty;
        public string Liquidacion { get; set; } = string.Empty;
        public decimal Valuacion { get; set; }
        public string Contraparte { get; set; } = string.Empty;
        public string Nego_Estruc { get; set; } = string.Empty;


    }
}
