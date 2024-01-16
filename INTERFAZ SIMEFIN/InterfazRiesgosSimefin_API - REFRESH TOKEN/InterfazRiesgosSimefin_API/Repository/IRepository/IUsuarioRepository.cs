using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using System.Security.Claims;

namespace InterfazRiesgosSimefin_API.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        bool IsUsuarioUnico(string userName);
        Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO);
        Task<TokenDTO> RefreshAccessToken(TokenDTO tokenDTO);



    }
}
