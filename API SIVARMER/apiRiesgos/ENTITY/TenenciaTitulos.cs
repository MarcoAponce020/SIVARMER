using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class TenenciaTitulos
    {
        public int FechaValuacion { get; set; }
        public string Emision { get; set; }
        public long Titulos { get; set; }
        public decimal ImporteSucio { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Portafolio { get; set; }
        public string NumOperacion { get; set; }
        public decimal InteresDevengado { get; set; }
        public long TitulosGarantia { get; set; }
        public decimal Intercupon { get; set; }
        public int DXVencer { get; set; }
        public int DXVRC0103 { get; set; }
        public decimal PrecioSucio { get; set; }
        public decimal PrecioLimpio { get; set; }
        public decimal ImporteLimpio { get; set; }
        public decimal ImporteInteres { get; set; }
        public decimal PrecioMercado { get; set; }
        public decimal ImporteMercado { get; set; }
        public int DXVRC02 { get; set; }
        public decimal PlusMinus { get; set; }
        public decimal TasaCosto { get; set; }
        public string Parte { get; set; }
    }
}
