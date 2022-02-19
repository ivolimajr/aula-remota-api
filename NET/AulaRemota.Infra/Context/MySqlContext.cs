using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
        public DbSet<UserModel> Usuario { get; set; }
        public DbSet<AddressModel> Endereco { get; set; }
        public DbSet<PhoneModel> Telefone { get; set; }
        public DbSet<FileModel> Arquivo { get; set; }

        //EDRIVING
        public DbSet<EdrivingModel> Edriving { get; set; }
        public DbSet<EdrivingLevelModel> EdrivingCargo { get; set; }

        // PARCEIRO
        public DbSet<PartnnerModel> Parceiro { get; set; }
        public DbSet<PartnnerLevelModel> ParceiroCargo { get; set; }

        //AUTO ESCOLA
        public DbSet<DrivingSchoolModel> AutoEscola { get; set; }
        public DbSet<AdministrativeModel> Administrativo { get; set; }
        public DbSet<InstructorModel> Instrutor { get; set; }

        public DbSet<StudentModel> Aluno { get; set; }
        public DbSet<CourseModel> Curso { get; set; }
        public DbSet<TurmaModel> Turma { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).GetType().Assembly);
            modelBuilder.Entity<PartnnerLevelModel>().HasData(
                new PartnnerLevelModel { Id = 1, Cargo = Constants.ParceiroCargos.ADMINISTRATIVO },
                new PartnnerLevelModel { Id = 2, Cargo = Constants.ParceiroCargos.ANALISTA },
                new PartnnerLevelModel { Id = 3, Cargo = Constants.ParceiroCargos.DIRETOR}
            );
            modelBuilder.Entity<EdrivingLevelModel>().HasData(
                new EdrivingLevelModel { Id = 1, Cargo = Constants.EdrivingCargos.ADMINISTRATIVO },
                new EdrivingLevelModel { Id = 2, Cargo = Constants.EdrivingCargos.ANALISTA },
                new EdrivingLevelModel { Id = 3, Cargo = Constants.EdrivingCargos.DIRETOR }
            );
        }

    }
}
