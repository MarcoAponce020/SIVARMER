using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class ComprasMesaDinero
    {
        public string FechaValor { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public string NumOperacion { get; set; } = string.Empty;
        public string Contraparte { get; set; } = string.Empty;
        public string Papel { get; set; } = string.Empty;
        public string Emision { get; set; } = string.Empty;
        public string Posicion { get; set; } = string.Empty;
        public string FV { get; set; } = string.Empty;
        public long NumTitulos { get; set; }
        public decimal PrecioSucio { get; set; }
        public decimal TasaRend { get; set; }
        public decimal TasaDiaria { get; set; }
        public int Plazo { get; set; }
        public decimal ImporteReal { get; set; }
        public string Portafolio { get; set; } = string.Empty;
    }
}
