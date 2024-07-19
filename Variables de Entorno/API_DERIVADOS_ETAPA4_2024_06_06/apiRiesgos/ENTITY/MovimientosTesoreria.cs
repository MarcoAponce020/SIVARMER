using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class MovimientosTesoreria
    {
        public int Identificador { get; set; }
        public string NomInstrumento { get; set; } = string.Empty;
        public string Emisor { get; set; } = string.Empty;
        public string Emision { get; set; } = string.Empty;
        public string ClaveOper { get; set; } = string.Empty;
        public string TipoMov { get; set; } = string.Empty;
        public long NumTitAsig { get; set; }
        public decimal PrecioRef { get; set; }
        public decimal PrecioLibros { get; set; }
        public decimal ImporteAsig { get; set; }
        public decimal TasaCosto { get; set; }
        public int Plazo { get; set; }
        public int FechaAlta { get; set; }
        public int FechaVen { get; set; }
        public long TitGarant { get; set; }
        public int Periodo { get; set; }
        public string TasaRef { get; set; } = string.Empty;
        public string EmisionGar { get; set; } = string.Empty;
        public string NumFuncionario { get; set; } = string.Empty;
        public string NumContraparte { get; set; } = string.Empty;
        public long NumOper { get; set; }
        public int FechaExp { get; set; }
    }
}
