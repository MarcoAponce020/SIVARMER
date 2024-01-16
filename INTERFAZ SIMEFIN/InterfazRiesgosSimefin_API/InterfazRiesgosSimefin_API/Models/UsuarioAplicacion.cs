using Microsoft.AspNetCore.Identity;

namespace InterfazRiesgosSimefin_API.Models
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string Nombres {  get; set; }   
    }
}
