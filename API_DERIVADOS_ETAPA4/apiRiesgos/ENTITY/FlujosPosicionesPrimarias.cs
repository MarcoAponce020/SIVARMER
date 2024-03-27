using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class FlujosPosicionesPrimarias
    {
        public int TipoPos { get; set; }
        public int? Fechareg { get; set; }
        public string NomPos { get; set; }
        public string? HoraReg { get; set; }
        public int CPosicion { get; set; }
        public string COperacion { get; set; }
        public string T_Pata { get; set; }
        public int F_Fixing { get; set; }
        public int F_Inicio { get; set; }
        public int F_Final { get; set; }
        public int F_Desc { get; set; }
        public string Pago_int { get; set; }
        public string Int_S_Saldo { get; set; }
        public decimal Saldo { get; set; }
        public decimal Amortizacion { get; set; }
        public decimal T_Aplicar { get; set; }

    }
}
