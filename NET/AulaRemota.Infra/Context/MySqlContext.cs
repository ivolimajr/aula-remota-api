using AulaRemota.Core.Entity;
using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Entity.Auto_Escola;
using AulaRemota.Core.Models;
using AulaRemota.Infra.Configuracoes;
using AulaRemota.Infra.Configuracoes.Auto_Escola;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        //GERAL
        public DbSet<ApiUserModel> ApiUser { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }
        public DbSet<TelefoneModel> Telefone { get; set; }

        //EDRIVING
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

        //CONFIGURAÇÕES DAS TABELAS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //USUARIO DA API
            modelBuilder.ApplyConfiguration(new ApiUserConfiguracoes());

            //EDRIVING
            modelBuilder.ApplyConfiguration(new EdrivingConfiguracoes());
            modelBuilder.ApplyConfiguration(new EdrivingCargoConfiguracoes());

            //PARCEIRO
            modelBuilder.ApplyConfiguration(new ParceiroCargoConfiguracoes());
            modelBuilder.ApplyConfiguration(new ParceiroConfiguracoes());

            //GERAL
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
            modelBuilder.ApplyConfiguration(new TelefoneConfiguracoes());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguracoes());

            //AUTO ESCOLA
            modelBuilder.ApplyConfiguration(new AutoEscolaConfiguracoes());
            modelBuilder.ApplyConfiguration(new InstrutorConfiguracoes());
            modelBuilder.ApplyConfiguration(new AdministrativoConfiguracoes());
            modelBuilder.ApplyConfiguration(new TurmaConfiguracoes());
            modelBuilder.ApplyConfiguration(new CursoConfiguracoes());
            modelBuilder.ApplyConfiguration(new AlunoConfiguracoes());
            modelBuilder.ApplyConfiguration(new ArquivoConfiguracoes());

        }

    }
}
