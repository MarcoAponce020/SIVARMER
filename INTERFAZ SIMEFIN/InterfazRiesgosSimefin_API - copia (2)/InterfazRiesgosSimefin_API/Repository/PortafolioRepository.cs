using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Repository.IRepository;

namespace InterfazRiesgosSimefin_API.Repository
{
    public class PortafolioRepository : Repository<Portafolio>, IPortafolioRepository
    {
        private readonly ApplicationDbContext _db;
        public PortafolioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Portafolio> Actualizar(Portafolio entidad)
        {
            entidad.FechaModificacion = DateTime.Now;
            _db.Portafolios.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;

        }
    }
}
