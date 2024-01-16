using InterfazRiesgosSimefin_Utilidad;
using InterfazRiesgosSimefin_WEB.Models;
using InterfazRiesgosSimefin_WEB.Models.Dto;
using InterfazRiesgosSimefin_WEB.Services.IServices;

namespace InterfazRiesgosSimefin_WEB.Services
{
    public class PortafolioService : BaseServices, IPortafolioService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _portafolioURL;

        public PortafolioService(IHttpClientFactory httpClient, IConfiguration configuration) :base(httpClient)
        {
            _httpClient = httpClient;
            _portafolioURL = configuration.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Actualizar<T>(PortafolioUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                URL = _portafolioURL + "/api/Portafolios/"+dto.IdPortafolio
            });
        }

        public Task<T> Crear<T>(PortafolioCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                URL = _portafolioURL+"/api/Portafolios"
            });
        }

        public Task<T> Obtener<T>(int idPortafolio)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                URL = _portafolioURL + "/api/Portafolios/" + idPortafolio
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                URL = _portafolioURL + "/api/Portafolios" 
            });
        }

        public Task<T> Remover<T>(int idPortafolio)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                URL = _portafolioURL + "/api/Portafolios/" + idPortafolio
            });
        }
    }
}
