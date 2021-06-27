using AulaRemota.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.Data
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SEED DATA CARGO DA PLATAFORMA -> EDRIVING
            modelBuilder.Entity<EdrivingCargo>().HasData(
                new EdrivingCargo { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargo { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargo { Id = 3, Cargo = "ADMINISTRATIVO" }
                );

            //SEED DATA CARGO DO DETRAN -> PARCEIRO
            modelBuilder.Entity<ParceiroCargo>().HasData(
                new EdrivingCargo { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargo { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargo { Id = 3, Cargo = "ADMINISTRATIVO" }
                );

            modelBuilder.Entity<Edriving>().HasOne(e => e.Cargo);
            modelBuilder.Entity<Edriving>().HasOne(e => e.Usuario);

            modelBuilder.Entity<Parceiro>().HasOne(e => e.Cargo);
            modelBuilder.Entity<Parceiro>().HasOne(e => e.Usuario);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Edriving> Edriving { get; set; }
        public DbSet<EdrivingCargo> EdrivingCargo { get; set; }
        public DbSet<Parceiro> Parceiro { get; set; }
        public DbSet<ParceiroCargo> ParceiroCargo { get; set; }

    }
}
