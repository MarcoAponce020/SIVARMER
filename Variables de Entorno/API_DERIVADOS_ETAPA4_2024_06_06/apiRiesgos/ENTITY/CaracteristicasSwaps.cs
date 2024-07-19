using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class CaracteristicasSwaps
    {
        public int Fecha_Pos { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public string Cve_Inst { get; set; } = string.Empty;
        public string Cve_Prov { get; set; } = string.Empty;
        public string Numsec { get; set; } = string.Empty;
        public string Cvecontpp { get; set; } = string.Empty;
        public int Fec_Operac { get; set; }
        public int Fec_Ini { get; set; }
        public int Fec_Venc { get; set; }
        public string Tipo_Valor { get; set; } = string.Empty;
        public string Tipo_Oper { get; set; } = string.Empty;
        public int? Fec_Prox_Flujo { get; set; }
        public int Periodo_Flujo { get; set; }
        public string Base_Cal_Tasa { get; set; } = string.Empty;
        public string Cvemoneda { get; set; } = string.Empty;
        public decimal Tipo_Cambio_Rec { get; set; }
        public decimal Tasa_Recibe { get; set; }
        public decimal M_Noc_Flujo { get; set; }
        public string Formula_Flujo { get; set; } = string.Empty;
        public int? F_Prox_Flujo_Ent { get; set; }
        public int Periodicidad { get; set; }
        public string Base_Calculo { get; set; } = string.Empty;
        public string Tipo_Moneda { get; set; } = string.Empty;
        public decimal Tipo_Cambio_Ent { get; set; }
        public decimal M_Noc_Flujo_Ent { get; set; }
        public string Tasa_Ref_Activa { get; set; } = string.Empty;
        public int? T_Ref_Activa_Dias_Ante { get; set; }
        public string Tasa_Ref_Pasiva { get; set; } = string.Empty;
        public int? T_Ref_Pasiva_Dias_Ante { get; set; }
        public decimal Sobretasa_Activa { get; set; }
        public string Op_Sobretasa_Activa { get; set; } = string.Empty;
        public decimal Sobretasa_Pasiva { get; set; }
        public string Op_Sobretasa_Pasiva { get; set; } = string.Empty;
        public decimal Tasa_Entrega { get; set; }
        public decimal Prima { get; set; }
        public string Objetivo_Oper { get; set; } = string.Empty;
        public decimal Val_Activa { get; set; }
        public decimal Val_Pasiva { get; set; }
        public decimal Valor_Neto { get; set; }
        public decimal Marca_Mercado { get; set; }
        public string Id_Side { get; set; } = string.Empty;
        public string Llama_Margen { get; set; } = string.Empty;
        public string Nego_Estruc { get; set; } = string.Empty;
        public string Reinvierte_int_Act { get; set; } = string.Empty;
        public string Reinvierte_int_Pas { get; set; } = string.Empty;

        public string UTI { get; set; } = string.Empty;


    }
}
