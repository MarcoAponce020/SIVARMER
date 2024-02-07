using InterfazRiesgosSimefin_API.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterfazRiesgosSimefin_API.Models
{
    public class Portafolio
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPortafolio { get; set; }

        [Required]
        public string? F_Posicion { get; set; }

        [Required]
        public string? NombrePortafolio { get; set; }

        [Required]
        public string? SubPortafolio { get; set; }

        public string? listaDatos { get; set; }
        public int No_Envio { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [Required]
        public int SubPortafolioId { get; set; } = 0;

    }


}
