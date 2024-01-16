using static InterfazRiesgosSimefin_Utilidad.DS;

namespace InterfazRiesgosSimefin_WEB.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;
        public string URL { get; set; }

        public object Datos {  get; set; }
    }
}
