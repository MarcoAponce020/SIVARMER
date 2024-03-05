using System.ComponentModel.DataAnnotations;

namespace InterfazRiesgosSimefin_API.Models.Dto
{
    public class RegistroRequestDTO
    {

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = string.Empty;
    }
}
