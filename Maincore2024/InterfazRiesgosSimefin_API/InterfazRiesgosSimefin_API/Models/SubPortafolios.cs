using System.ComponentModel.DataAnnotations;

namespace InterfazRiesgosSimefin_API.Models
{
    public class SubPortafolios
    {

        [Key]
        public int SubPortafolioId { get; set; }

        public string? Descripcion { get; set; }

    }
}
