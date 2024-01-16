using System.ComponentModel.DataAnnotations;

namespace InterfazRiesgosSimefin_API.Models
{
    public class RefreshToken
    {

        [Key]
        public int idRefresh { get; set; }
        public string idUsuario { get; set; }

        public string tokenId { get; set; }

        public string refreshToken { get; set; }
        //Nos aseguraremos de que el token de actualización solo sea válido para un uso.
        public bool isValid { get; set; }
        public DateTime TiempoExpiracion { get; set; }

    }
}
