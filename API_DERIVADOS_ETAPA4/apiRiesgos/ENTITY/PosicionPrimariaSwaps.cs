using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class PosicionPrimariaSwaps
    {
        public int Tipopos { get; set; }
        public int? Fechareg { get; set; }
        public string Nompos { get; set; }
        public string Horareg { get; set; }
        public int CPosicion { get; set; }
        public string COperacion { get; set; }
        public int F_Inicio { get; set; }
        public int F_Vencimiento { get; set; }
        public string Inter_I { get; set; }
        public string Inter_F { get; set; }
        public string Ac_Int_Act { get; set; }
        public string Ac_Int_Pas { get; set; }
        public string Tc_Activa { get; set; }
        public string Tc_Pasiva { get; set; }
     //   public int D_Ante_Activa { get; set; }
        public string Conv_Int_Act { get; set; }
        public string Conv_Int_Pas { get; set; }
        public string C_Producto { get; set; }
        public string F_Valuacion { get; set; }
        public string Id_Contrap { get; set; }
        public string Id_Banxico { get; set; }
        public string Llama_Margen { get; set; }
        public string Intencion { get; set; }
        public string Estructural { get; set; }
        public string Cal_F_T_Activa { get; set; }
        public string Cal_F_T_Pasiva { get; set; }
        public string Cal_Liq_Activa { get; set; }
        public string Cal_Liq_Pasiva { get; set; }
        public string Px_Swap { get; set; }
        public string Colateral { get; set; }
        //public string Ref_Pos_Cob { get; set; }
        //public string Tipo_Pos_Cob { get; set; }
        //public decimal Porc_Cob { get; set; }
        //public int D_Ante_Pasiva { get; set; }
        //public string Op_St_Activa { get; set; }
        //public string Op_St_Pasiva { get; set; }
        public decimal? St_Activa { get; set; }
        public decimal? St_Pasiva { get; set; }
        //public string Moneda_Act { get; set; }
        //public string Moneda_Pas { get; set; }

    }
}
