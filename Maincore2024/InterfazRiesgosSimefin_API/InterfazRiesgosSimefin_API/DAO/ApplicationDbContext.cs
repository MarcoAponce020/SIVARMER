using InterfazRiesgosSimefin_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.isValid).HasComputedColumnSql("(case when [TiempoExpiracion]<getutcdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
