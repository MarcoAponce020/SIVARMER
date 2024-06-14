using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class PosicionCalculoVAR
    {
        public string Fecha { get; set; }
        public string Intencion { get; set; }
        public int TipoOperacion { get; set; }
        public string TipoValor { get; set; }
        public string Emision { get; set; }
        public string Serie { get; set; }
        public int FechaVto { get; set; }
        public decimal TasaCupon { get; set; }
        public int D_vto { get; set; }
        public int DxV { get; set; }
        public int TCupon { get; set; }
       //Cambia nombre de Premio a TasaPremio
        public decimal Premio { get; set; }
        public long Titulos { get; set; }
        public int TipoPosicion { get; set; }

        //Se cambia tipo de dato public int PrecioCompra { get; set; }
       // public int PrecioCompra { get; set; }
        
        public decimal PrecioCompra { get; set; }
        public int FechaCompra { get; set; }
        public string Mercado { get; set; }
        public int NumPortafolio { get; set; }
        public string Portafolio { get; set; }
        //public int DxV { get; set; }
        
    }
}
