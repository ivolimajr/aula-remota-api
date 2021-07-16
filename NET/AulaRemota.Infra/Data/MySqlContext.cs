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
            modelBuilder.Entity<EdrivingCargoModel>().HasData(
                new EdrivingCargoModel { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargoModel { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargoModel { Id = 3, Cargo = "ADMINISTRATIVO" }
                );

            //SEED DATA CARGO DO DETRAN -> PARCEIRO
            modelBuilder.Entity<ParceiroCargoModel>().HasData(
                new EdrivingCargoModel { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargoModel { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargoModel { Id = 3, Cargo = "ADMINISTRATIVO" }
                );

            modelBuilder.Entity<EdrivingModel>().HasOne(e => e.Cargo);
            modelBuilder.Entity<EdrivingModel>().HasOne(e => e.Usuario);

            modelBuilder.Entity<ParceiroModel>().HasOne(e => e.Cargo);
            modelBuilder.Entity<ParceiroModel>().HasOne(e => e.Usuario);
        }

        //AUTH
        public DbSet<AuthUserModel> AuthUser { get; set; }

        //USUARIO
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }

        // EDRIVING
        public DbSet<EdrivingModel> Edriving { get; set; }
        public DbSet<EdrivingCargoModel> EdrivingCargo { get; set; }

        // PARCEIRO
        public DbSet<ParceiroModel> Parceiro { get; set; }
        public DbSet<ParceiroCargoModel> ParceiroCargo { get; set; }


    }
}
