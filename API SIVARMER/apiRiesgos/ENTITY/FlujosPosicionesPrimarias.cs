using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class FlujosPosicionesPrimarias
    {
        public int TIPOPOS { get; set; }
        public int FECHAREG { get; set; }
        public string NOMPOS { get; set; }
        public string HORAREG { get; set; }
        public int CPOSICION { get; set; }
        public string COPERACION { get; set; }
        public string T_PATA { get; set; }
        public int F_FIXING { get; set; }
        public int F_INICIO { get; set; }
        public int F_FINAL { get; set; }
        public int F_DESC { get; set; }
        public string PAGO_INT { get; set; }
        public string INT_S_SALDO { get; set; }
        public decimal SALDO { get; set; }
        public decimal AMORTIZACION { get; set; }
        public decimal T_APLICAR { get; set; }
    }
}
