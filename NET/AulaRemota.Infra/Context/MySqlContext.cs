using AulaRemota.Core.Entity;
using AulaRemota.Infra.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApiUserConfiguracoes());
            modelBuilder.ApplyConfiguration(new EdrivingConfiguracoes());
            modelBuilder.ApplyConfiguration(new EdrivingCargoConfiguracoes());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
            modelBuilder.ApplyConfiguration(new TelefoneConfiguracoes());

            //SEED DATA CARGO DO DETRAN -> PARCEIRO
            modelBuilder.Entity<ParceiroCargoModel>().HasData(
                new EdrivingCargoModel { Id = 1, Cargo = "DIRETOR" },
                new EdrivingCargoModel { Id = 2, Cargo = "ANALISTA" },
                new EdrivingCargoModel { Id = 3, Cargo = "ADMINISTRATIVO" }
                );

        }

        public DbSet<EnderecoModel> Endereco { get; set; }
/*        // PARCEIRO
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
*/


    }
}
