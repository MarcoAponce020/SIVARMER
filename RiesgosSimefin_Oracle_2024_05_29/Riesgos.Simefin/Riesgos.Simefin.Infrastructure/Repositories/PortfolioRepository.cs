using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Riesgos.Simefin.Application.Interfaces;
using Riesgos.Simefin.Domain.Entities;
using Riesgos.Simefin.Infrastructure.Persistence;
using System.Configuration;

namespace Riesgos.Simefin.Infrastructure.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly SimefinDBContext _context;
        private readonly IConfiguration _configuration;

        public PortfolioRepository(SimefinDBContext context, IConfiguration configuration)
        {
            _context = context; 
            _configuration = configuration;
        }

        public async Task<IEnumerable<Portafolio>> GetAllAsync()
        {
            try
            {
                var data = await _context.Portafolios.ToListAsync();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Portafolio> GetByIdAsync(int id)
        {
            return await _context.Portafolios.FindAsync(id);
        }

        public async Task<int> AddAsync(Portafolio entity)
        {
            await _context.Portafolios.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.IdPortafolio;
        }

        public async Task<bool> UpdateAsync(Portafolio entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Portafolios.FindAsync(id);
            _context.Portafolios.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
