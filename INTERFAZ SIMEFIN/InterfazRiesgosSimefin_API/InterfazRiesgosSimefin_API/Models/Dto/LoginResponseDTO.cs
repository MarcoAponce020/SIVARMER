using System.Net;
using System.Security.Claims;

namespace InterfazRiesgosSimefin_API.Models.Dto
{
    public class LoginResponseDTO
    {
 
        public Usuario Usuario { get; set; } 
        public string Token { get; set; }



    }

}
