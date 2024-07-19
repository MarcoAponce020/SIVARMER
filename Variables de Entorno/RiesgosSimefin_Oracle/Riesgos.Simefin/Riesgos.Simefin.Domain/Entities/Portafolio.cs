using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riesgos.Simefin.Domain.Entities
{
    public class Portafolio
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPortafolio { get; set; }


        [Required]
        public string? F_Posicion { get; set; }

        //public IEnumerable<DataList> ListaDatos { get; set; } = new List<DataList>();

        [Required]
        public string? NombrePortafolio { get; set; }
        public string? SubPortafolio { get; set; }

        public int No_Envio { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [Required]
        public int SubPortafolioId { get; set; } = 0;
        public dynamic? ListaDatos { get; set; }

    }

    public class DataList
    {

        public string? Fecha { get; set; }

        public string? Valor { get; set; }

    }

}
