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
        public string NomInstrumento { get; set; }
        public string Emisor { get; set; }
        public string Emision { get; set; }
        public string ClaveOper { get; set; }
        public string TipoMov { get; set; }
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
        public string TasaRef { get; set; }
        public string EmisionGar { get; set; }
        public string NumFuncionario { get; set; }
        public string NumContraparte { get; set; }
        public long NumOper { get; set; }
        public int FechaExp { get; set; }
    }
}
