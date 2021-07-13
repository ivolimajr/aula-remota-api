using AulaRemota.Core.Entity;
using AulaRemota.Core.Entity.Auth;
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

            modelBuilder.Entity<EdrivingModel>().HasOne(e => e.Cargo);
            modelBuilder.Entity<EdrivingModel>().HasOne(e => e.Usuario);

            modelBuilder.Entity<Parceiro>().HasOne(e => e.Cargo);
            modelBuilder.Entity<Parceiro>().HasOne(e => e.Usuario);
        }

        //AUTH
        public DbSet<AuthUser> AuthUser { get; set; }

        //USUARIO
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        // EDRIVING
        public DbSet<EdrivingModel> Edriving { get; set; }
        public DbSet<EdrivingCargo> EdrivingCargo { get; set; }

        // PARCEIRO
        public DbSet<Parceiro> Parceiro { get; set; }
        public DbSet<ParceiroCargo> ParceiroCargo { get; set; }


    }
}
