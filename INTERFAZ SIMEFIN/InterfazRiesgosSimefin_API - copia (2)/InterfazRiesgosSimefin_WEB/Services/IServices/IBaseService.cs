using InterfazRiesgosSimefin_WEB.Models;

namespace InterfazRiesgosSimefin_WEB.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
