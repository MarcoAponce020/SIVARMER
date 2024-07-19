using System.ComponentModel.DataAnnotations;

namespace Riesgos.Simefin.Application.DTOs.User
{

    public class TokenDTO
    {

        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;

    }

}
