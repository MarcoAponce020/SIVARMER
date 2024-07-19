namespace ENTITY
{
    public class FlujosPosicionesPrimarias
    {
        public decimal Amortizacion { get; set; }
        public string COperacion { get; set; } = string.Empty;
        public int CPosicion { get; set; }
        public int? Fechareg { get; set; }
        public int FechaLiq { get; set; }
        public int F_Desc { get; set; }
        public int F_Final { get; set; }
        public int F_Fixing { get; set; }
        public int F_Inicio { get; set; }
        public string? HoraReg { get; set; }
        public string Int_S_Saldo { get; set; } = string.Empty;
        public string NomPos { get; set; } = string.Empty;
        public string Pago_int { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int TipoPos { get; set; }
        public decimal T_Aplicar { get; set; }
        public string T_Pata { get; set; } = string.Empty;

    }
}
