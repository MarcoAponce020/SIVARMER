using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riesgos.Simefin.Domain.Entities
{
    public class SubPortafolios
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubPortafolioId { get; set; }

        public string? Descripcion { get; set; }

    }
}
