using InterfazRiesgosSimefin_API.Models;

namespace InterfazRiesgosSimefin_API.Repository.IRepository
{
    public interface IPortafolioRepository : IRepository<Portafolio>
    {
        Task<Portafolio> Actualizar(Portafolio entidad);
    }
}
