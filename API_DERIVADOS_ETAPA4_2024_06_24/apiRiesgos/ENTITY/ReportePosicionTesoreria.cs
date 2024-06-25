using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class ReportePosicionTesoreria
    {
        public string Emision { get; set; }
        
        public string Posicion { get; set; }
        public string Operacion { get; set; }
        public int FechaCorte { get; set; }
        public long Titulos { get; set; }
        public decimal PrecioMercado { get; set; }
        public int FechaVto { get; set; }
        public decimal TasaMercado { get; set; }
        public decimal PrecioLibros { get; set; }
        public decimal TasaCupon { get; set; }
        public decimal ValorNominal { get; set; }
        public int FechaEmision { get; set; }
        public int FechaVtoCup { get; set; }
        public int ClaveInstrumento { get; set; }
        public string ClaveEmisor { get; set; }
        //public decimal Duracion { get; set; }
        public int Duracion { get; set; }
        public string TipoTasa { get; set; }
        public string Moneda { get; set; }
        public string Area { get; set; }
        public string Mercado { get; set; }
        public string Contrato { get; set; }
        public int PlazoCupon { get; set; }
        public int PlazoRepo { get; set; }
        public bool Udizado { get; set; }
    }
}
