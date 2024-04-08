using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class PosicionesPrimForwards
    {
        public int FPosicion { get; set; }
        public string CIkos { get; set; }
        public int F_inicio { get; set; }
        public int F_ven { get; set; }
        public int F_Liq { get; set; }
        public decimal Monto_Base_Act { get; set; }
        public decimal Monto_Fwd_Act { get; set; }
        public string Moneda_Act { get; set; }
        public string Curva_Val_Activo { get; set; }
        public decimal Monto_Base_Pas { get; set; }
        public decimal Monto_Fwd_Pas { get; set; }
        public string Moneda_Pas { get; set; }
        public string Curva_Val_Pasivo { get; set; }



    }
}
