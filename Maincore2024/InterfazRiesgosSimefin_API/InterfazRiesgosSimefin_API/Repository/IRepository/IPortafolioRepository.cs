using InterfazRiesgosSimefin_API.Models;
using System.Text;

namespace InterfazRiesgosSimefin_API.Repository.IRepository
{
    public interface IPortafolioRepository : IRepository<Portafolio>
    {
        Task<Portafolio> Actualizar(Portafolio entidad);

        Task<APIResponse> ExcelLoad(IFormFile file);
    }
}
