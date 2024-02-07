using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterfazRiesgosSimefin_API.Models.Dto
{
    public class PortafolioDto
    {
       
        public int IdPortafolio { get; set; }
        [Required]
        public string? F_Posicion { get; set; }

        [Required]
        [MaxLength(30)]
        public string? NombrePortafolio { get; set; }
        public string? SubPortafolio { get; set; }

        //public dynamic? listaDatos { get; set; }

        public int No_Envio { get; set; }
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [Required]
        public int SubPortafolioId { get; set; }

        public dynamic? listaDatos { get; set; }

    }

    public class Datos
    {
        public string? Fecha { get; set; }
        public string? valor { get; set; }
    }

}
