using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class ComprasMesaDinero
    {
        public string FechaValor { get; set; }
        public string Moneda { get; set; }
        public string NumOperacion { get; set; }
        public string Contraparte { get; set; }
        public string Papel { get; set; }
        public string Emision { get; set; }
        public string Posicion { get; set; }
        public string FV { get; set; }
        public long NumTitulos { get; set; }
        public decimal PrecioSucio { get; set; }
        public decimal TasaRend { get; set; }
        public decimal TasaDiaria { get; set; }
        public int Plazo { get; set; }
        public decimal ImporteReal { get; set; }
        public string Portafolio { get; set; }
    }
}
