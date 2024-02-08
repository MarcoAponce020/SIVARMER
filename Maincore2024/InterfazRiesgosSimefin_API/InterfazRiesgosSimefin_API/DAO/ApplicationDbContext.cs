using InterfazRiesgosSimefin_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterfazRiesgosSimefin_API.DAO

{
    public class ApplicationDbContext :IdentityDbContext<UsuarioAplicacion>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
        }

        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        public DbSet<Portafolio> Portafolios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<SubPortafolios> SubPortafolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
