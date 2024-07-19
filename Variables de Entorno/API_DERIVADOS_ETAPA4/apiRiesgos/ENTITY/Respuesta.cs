namespace ENTITY
{
    public class Respuesta
    {
        public bool exito { get; set; }

        public string mensaje { get; set; } = string.Empty;

        public List<string> ListaMensajes { get; set; } = new List<string>();

    }
}
