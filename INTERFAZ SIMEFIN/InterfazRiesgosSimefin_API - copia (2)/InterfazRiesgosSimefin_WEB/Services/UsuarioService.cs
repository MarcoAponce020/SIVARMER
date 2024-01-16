using InterfazRiesgosSimefin_WEB.Models.Dto;
using InterfazRiesgosSimefin_Utilidad;
using InterfazRiesgosSimefin_WEB.Services.IServices;
using static InterfazRiesgosSimefin_Utilidad.DS;
using System.Security.Policy;

namespace InterfazRiesgosSimefin_WEB.Services
{
    public class UsuarioService : BaseServices, IUsuarioService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _portafolioURL;
        public UsuarioService(IHttpClientFactory httpClient, IConfiguration configuration) :base(httpClient)
        {
            _httpClient = httpClient;
            _portafolioURL = configuration.GetValue<string>("ServiceUrls:API_URL");

        }
        Task<T> IUsuarioService.Login<T>(LoginRequestDTO dto)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                URL = _portafolioURL + "/api/usuario/login"
            });
        }

        Task<T> IUsuarioService.Registrar<T>(RegistroRequestDTO dto)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                URL = _portafolioURL + "/api/usuario/registrar"
            });
        }
    }
}
