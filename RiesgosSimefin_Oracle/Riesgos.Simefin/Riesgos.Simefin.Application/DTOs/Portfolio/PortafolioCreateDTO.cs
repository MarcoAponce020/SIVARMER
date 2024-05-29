using System.ComponentModel.DataAnnotations;

namespace Riesgos.Simefin.Application.DTOs.Portfolio
{

    public class PortafolioCreateDTO
    {

        //public DateTime FechaCreacion { get; set; }

        //public DateTime FechaModificacion { get; set; }

        public string? F_Posicion { get; set; }

        public string? ListaDatos { get; set; }

        [Required]
        [MaxLength(30)]
        public string? NombrePortafolio { get; set; }

        //public int No_Envio { get; set; }

        //public string? SubPortafolio { get; set; }

        [Required]
        public int SubPortafolioId { get; set; }

    }

}
