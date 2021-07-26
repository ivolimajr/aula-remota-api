using AulaRemota.Core.Entity;
using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Entity.Auto_Escola;
using AulaRemota.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.Context
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

        }
        
        //AUTH
        public DbSet<AuthUserModel> AuthUser { get; set; }

        //USUARIO
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<TelefoneModel> Telefone { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }

        // EDRIVING
        public DbSet<EdrivingModel> Edriving { get; set; }
        public DbSet<EdrivingCargoModel> EdrivingCargo { get; set; }

        // PARCEIRO
        public DbSet<ParceiroModel> Parceiro { get; set; }
        public DbSet<ParceiroCargoModel> ParceiroCargo { get; set; }

        //AUTO ESCOLA
        public DbSet<AdministrativoModel> Administrativo { get; set; }
        public DbSet<AutoEscolaModel> AutoEscola { get; set; }
        public DbSet<AlunoModel> Aluno { get; set; }
        public DbSet<AutoEscolaCargoModel> AutoEscolaCargo { get; set; }
        public DbSet<CursoModel> Curso { get; set; }
        public DbSet<InstrutorModel> Instrutor { get; set; }
        public DbSet<TurmaModel> Turma { get; set; }
        public DbSet<ArquivoModel> Arquivo { get; set; }


    }
}
