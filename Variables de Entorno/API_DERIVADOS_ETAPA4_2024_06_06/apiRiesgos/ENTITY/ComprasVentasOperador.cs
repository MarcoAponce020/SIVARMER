namespace ENTITY
{
    public class ComprasVentasOperador
    {
        public int FechaConcertacion { get; set; }
        public string Contraparte { get; set; } = string.Empty;
        public string Contrato { get; set; } = string.Empty;
        public string TipoValor { get; set; } = string.Empty;
        public string Emisor { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public string ClaveOper { get; set; } = string.Empty;
        public string TipoOper { get; set; } = string.Empty;
        public decimal ImporteAsignado { get; set; }
        public decimal ImporteCierre { get; set; }
        public string Parte { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}
