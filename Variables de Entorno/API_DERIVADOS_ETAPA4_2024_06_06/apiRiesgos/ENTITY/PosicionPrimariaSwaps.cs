namespace ENTITY
{
    public class PosicionPrimariaSwaps
    {
        public int Tipopos { get; set; }
        public int? Fechareg { get; set; }
        public string Nompos { get; set; } = string.Empty;
        public string Horareg { get; set; } = string.Empty;
        public int CPosicion { get; set; }
        public string COperacion { get; set; } = string.Empty;
        public int F_Inicio { get; set; }
        public int F_Vencimiento { get; set; }
        public string Inter_I { get; set; } = string.Empty;
        public string Inter_F { get; set; } = string.Empty;
        public string Ac_Int_Act { get; set; } = string.Empty;
        public string Ac_Int_Pas { get; set; } = string.Empty;
        public string Tc_Activa { get; set; } = string.Empty;
        public string Tc_Pasiva { get; set; } = string.Empty;
        public int? D_Ante_Activa { get; set; }
        public string Conv_Int_Act { get; set; } = string.Empty;
        public string Conv_Int_Pas { get; set; } = string.Empty;
        public string C_Producto { get; set; } = string.Empty;
        public string F_Valuacion { get; set; } = string.Empty;
        public string Id_Contrap { get; set; } = string.Empty;
        public string Id_Banxico { get; set; } = string.Empty;
        public string Llama_Margen { get; set; } = string.Empty;
        public string Intencion { get; set; } = string.Empty;
        public string Estructural { get; set; } = string.Empty;
        public string Cal_F_T_Activa { get; set; } = string.Empty;
        public string Cal_F_T_Pasiva { get; set; } = string.Empty;
        public string Cal_Liq_Activa { get; set; } = string.Empty;
        public string Cal_Liq_Pasiva { get; set; } = string.Empty;
        public string Px_Swap { get; set; } = string.Empty;
        public string Colateral { get; set; } = string.Empty;
        public int? D_Ante_Pasiva { get; set; }
        public decimal? St_Activa { get; set; }
        public decimal? St_Pasiva { get; set; }



        public string Moneda_Act { get; set; } = string.Empty;
        public string Moneda_Pas { get; set; } = string.Empty;
        public string Op_St_Activa { get; set; } = string.Empty;
        public string Op_St_Pasiva { get; set; } = string.Empty;
        public decimal Porc_Cob { get; set; }
        public string Ref_Pos_Cob { get; set; } = string.Empty;
        public string Tipo_Pos_Cob { get; set; } = string.Empty;

    }
}
