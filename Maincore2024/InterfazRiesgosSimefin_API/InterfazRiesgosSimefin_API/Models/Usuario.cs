namespace InterfazRiesgosSimefin_API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}
