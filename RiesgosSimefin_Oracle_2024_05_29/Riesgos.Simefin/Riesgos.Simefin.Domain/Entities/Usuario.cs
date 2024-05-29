using System.ComponentModel.DataAnnotations.Schema;

namespace Riesgos.Simefin.Domain.Entities
{
    public class Usuario
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

    }
}
