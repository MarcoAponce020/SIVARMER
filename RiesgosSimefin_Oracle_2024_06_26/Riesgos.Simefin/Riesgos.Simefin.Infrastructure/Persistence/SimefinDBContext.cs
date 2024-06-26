using Microsoft.EntityFrameworkCore;

//using Oracle.EntityFrameworkCore;
using Riesgos.Simefin.Domain.Entities;
using System.Reflection.Metadata;

namespace Riesgos.Simefin.Infrastructure.Persistence
{

    public class SimefinDBContext : DbContext
    {


        public SimefinDBContext(DbContextOptions<SimefinDBContext> options) : base(options)
        {

        }


        //public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }

        public DbSet<Portafolio> Portafolios { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<SubPortafolios> SubPortafolios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Portafolio>(entity => {
            //    entity.ToTable("Portafolios");
            //});
            //modelBuilder.HasSequence("Portafolio_SEQ");


            //modelBuilder.Entity<RefreshToken>(entity =>
            //{
            //    entity.Property(e => e.isValid).HasComputedColumnSql("(case when [TiempoExpiracion]<getutcdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            //});

            //modelBuilder.Entity<Portafolio>().Property(p => p.IdPortafolio).UseIdentityColumn(); //.UseOracleIdentityColumn();

            base.OnModelCreating(modelBuilder);
        }

    }

}
