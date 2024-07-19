using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riesgos.Simefin.Domain.Entities
{
    public class Token
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TokenId { get; set; }

        public string? AccessToken { get; set; }

        public DateTime ExpirationDate { get; set; }

        //Nos aseguraremos de que el token de actualización solo sea válido para un uso.
        public bool IsValid { get; set; }

        public string? RefreshToken { get; set; }

        public string? UsuarioId { get; set; }

    }
}
