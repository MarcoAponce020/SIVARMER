using InterfazRiesgosSimefin_WEB.Models.Dto;

namespace InterfazRiesgosSimefin_WEB.Services.IServices
{
    public interface IUsuarioService
    {
        Task<T> Login<T>(LoginRequestDTO dto);
        Task<T> Registrar<T>(RegistroRequestDTO dto);

    }
}
