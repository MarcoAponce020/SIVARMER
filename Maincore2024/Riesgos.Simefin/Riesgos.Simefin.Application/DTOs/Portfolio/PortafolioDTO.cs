using System.ComponentModel.DataAnnotations;

namespace Riesgos.Simefin.Application.DTOs.Portfolio
{

    public class PortafolioDTO
    {

        public int IdPortafolio { get; set; }
        [Required]
        public string? F_Posicion { get; set; }

        [Required]
        [MaxLength(30)]
        public string? NombrePortafolio { get; set; }

        public string? SubPortafolio { get; set; }

        public dynamic? ListaDatos { get; set; }

        public int No_Envio { get; set; }
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [Required]
        public int SubPortafolioId { get; set; }

    }

}
