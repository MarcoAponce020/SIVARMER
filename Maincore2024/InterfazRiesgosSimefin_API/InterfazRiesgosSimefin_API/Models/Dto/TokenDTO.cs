using System.ComponentModel.DataAnnotations;

namespace InterfazRiesgosSimefin_API.Models.Dto
{
    public class TokenDTO
    {

        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;

    }

}
