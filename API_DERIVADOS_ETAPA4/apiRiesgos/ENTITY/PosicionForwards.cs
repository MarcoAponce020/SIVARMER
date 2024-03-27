using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class PosicionForwards
    {
        public int F_Posicion { get; set; }
        public string Clave_Op { get; set; }
        public string T_Operacion { get; set; }
        public decimal M_Nocional { get; set; }
        public int F_inicio { get; set; }
        public int F_Vencimiento { get; set; }
        public int F_Liquidacion { get; set; }
        public string Clave_Producto { get; set; }
        public int Plazo_Fwd { get; set; }
        public decimal Tc_Pactado { get; set; }
        public string Intencion { get; set; }
        public string Liquidacion { get; set; }
        public decimal Valuacion { get; set; }
        public string Contraparte { get; set; }
        public string Nego_Estruc { get; set; }


    }
}
