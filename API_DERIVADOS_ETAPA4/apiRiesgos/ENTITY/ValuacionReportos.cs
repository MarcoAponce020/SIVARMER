using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class ValuacionReportos
    {
        public int FechaValuacion { get; set; }
        public string Emision { get; set; }
        public string Tipo { get; set; }
        public int Fecha { get; set; }
        public string NumContrato { get; set; }
        public string NumReferencia { get; set; }
        public string MovimientoOper { get; set; }
        public string Parte { get; set; }
        public int Titulos { get; set; }
        public decimal PrecioOperacion { get; set; }
        public decimal ImporteOperacion { get; set; }
        public decimal ImporteLibros { get; set; }
        public int DiasTranscurridos { get; set; }
        public int DxV { get; set; }
        public decimal TasaVencimiento { get; set; }
        public decimal TasaDiaria { get; set; }
        public decimal TasaCurva { get; set; }
        public decimal Premio { get; set; }
        public decimal PrecioVector { get; set; }
        public decimal ValorMercado { get; set; }
        public int Portafolio { get; set; }
    }
}
