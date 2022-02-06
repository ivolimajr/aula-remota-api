using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using AulaRemota.Infra.Configuracoes;
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Infra.Context
{
    public class MySqlContext : DbContext
    {
        internal bool UseProvider { get; set; }
        public IConfiguration Configuration { get; }

        public MySqlContext(bool useProvider)
        {
            UseProvider = useProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (UseProvider)
            {
                var serverVersion = new MySqlServerVersion(new Version(5, 6, 23));
                optionsBuilder
                    .UseMySql(UnitOfWork.Configuration.GetConnectionString("MySQLConnLocal"), serverVersion)
                    //.UseMySql(Configuration.GetConnectionString("MySQLConnSandbox"), serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }
        public MySqlContext()
        {
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        //GERAL
        public DbSet<ApiUserModel> ApiUser { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }
        public DbSet<TelefoneModel> Telefone { get; set; }
        public DbSet<ArquivoModel> Arquivo { get; set; }

        //EDRIVING
        public DbSet<EdrivingModel> Edriving { get; set; }
        public DbSet<EdrivingCargoModel> EdrivingCargo { get; set; }

        // PARCEIRO
        public DbSet<ParceiroModel> Parceiro { get; set; }
        public DbSet<ParceiroCargoModel> ParceiroCargo { get; set; }

        //AUTO ESCOLA
        public DbSet<AutoEscolaModel> AutoEscola { get; set; }
        public DbSet<AdministrativoModel> Administrativo { get; set; }
        public DbSet<InstrutorModel> Instrutor { get; set; }

        public DbSet<AlunoModel> Aluno { get; set; }
        public DbSet<CursoModel> Curso { get; set; }
        public DbSet<TurmaModel> Turma { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).GetType().Assembly);
            modelBuilder.Entity<ParceiroCargoModel>().HasData(
                new ParceiroCargoModel { Id = 1, Cargo = Constants.ParceiroCargos.ADMINISTRATIVO },
                new ParceiroCargoModel { Id = 2, Cargo = Constants.ParceiroCargos.ANALISTA },
                new ParceiroCargoModel { Id = 3, Cargo = Constants.ParceiroCargos.DIRETOR}
            );
            modelBuilder.Entity<EdrivingCargoModel>().HasData(
                new EdrivingCargoModel { Id = 1, Cargo = Constants.EdrivingCargos.ADMINISTRATIVO },
                new EdrivingCargoModel { Id = 2, Cargo = Constants.EdrivingCargos.ANALISTA },
                new EdrivingCargoModel { Id = 3, Cargo = Constants.EdrivingCargos.DIRETOR }
            );
        }

    }
}
