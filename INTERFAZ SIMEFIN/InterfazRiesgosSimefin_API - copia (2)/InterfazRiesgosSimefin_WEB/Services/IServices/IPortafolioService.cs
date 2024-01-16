using InterfazRiesgosSimefin_WEB.Models.Dto;

namespace InterfazRiesgosSimefin_WEB.Services.IServices
{
    public interface IPortafolioService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener<T>(int idPortafolio);
        Task<T> Crear<T>(PortafolioCreateDto dto);
        Task<T> Actualizar<T>(PortafolioUpdateDto dto);
        Task<T> Remover<T>(int idPortafolio);

    }
}
