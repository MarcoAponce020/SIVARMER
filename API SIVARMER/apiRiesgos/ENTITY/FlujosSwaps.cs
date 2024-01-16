using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class FlujosSwaps
    {
        public int FECHA_POS { get; set; }
        public string CVESWAP { get; set; }
        public string POSICION { get; set; }
        public int SECUENCIA { get; set; }
        public string TIP_NEGOC { get; set; }
        public int FEC_INI { get; set; }
        public int FEC_TER { get; set; }
        public int FEC_INI_R { get; set; }
        public int FEC_TER_R { get; set; }
        public decimal VALOR_NOC { get; set; }
        public decimal AMORTIZACION { get; set; }
        public string TASA { get; set; }
        public string SPREAD { get; set; }
        public int CUPON { get; set; }
        public string CONVENCION { get; set; }
        public string NUMMON { get; set; }
        public string TIP_SWAP { get; set; }
        public string ID_SIDE { get; set; }
        public string INTERI { get; set; }
        public string INTERF { get; set; }
        public int  FEC_LIQ { get; set; }
        public string NEGO_ESTRUC { get; set; }
        public string PAGA_INT { get; set; }
    }
}
