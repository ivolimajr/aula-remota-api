using AulaRemota.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EdrivingCargo>().HasData(
                new EdrivingCargo { Id = 1, Cargo = "Diretor" },
                new EdrivingCargo { Id = 2, Cargo = "Analista" },
                new EdrivingCargo { Id = 3, Cargo = "Administrativo" }
                );

            modelBuilder.Entity<Edriving>().HasOne(e => e.Cargo);
            modelBuilder.Entity<Edriving>().HasOne(e => e.Usuario);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Edriving> Edriving { get; set; }
        public DbSet<EdrivingCargo> EdrivingCargo { get; set; }

    }
}
