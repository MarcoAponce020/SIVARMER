using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class ReporteREVAME
    {
        public string Fecha { get; set; }
        public string Portafolio { get; set; }
        public string Emision { get; set; }
        public long Titulos { get; set; }
        public decimal PrecioLimpio { get; set; }
        public decimal PrecioSucio { get; set; }
        public decimal ImporteLimpio { get; set; }
        public decimal ImporteSucio { get; set; }
        public decimal PrecioLimpioLib { get; set; }
        public decimal PrecioSucioLib { get; set; }
        public decimal ImporteLimpioLib { get; set; }
        public decimal ImporteSucioLib { get; set; }
        public decimal Valuacion { get; set; }
        public decimal PrecioMercado { get; set; }
    }
}
