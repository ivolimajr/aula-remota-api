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
                optionsBuilder
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
        public DbSet<UserModel> User { get; set; }
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<PhoneModel> Phone { get; set; }
        public DbSet<FileModel> File { get; set; }

        //EDRIVING
        public DbSet<EdrivingModel> Edriving { get; set; }
        public DbSet<EdrivingLevelModel> EdrivingLevel { get; set; }

        // PARCEIRO
        public DbSet<PartnnerModel> Partnner { get; set; }
        public DbSet<PartnnerLevelModel> PartnnerLevel { get; set; }

        //AUTO ESCOLA
        public DbSet<DrivingSchoolModel> DrivingSchool { get; set; }
        public DbSet<AdministrativeModel> Administrative { get; set; }
        public DbSet<InstructorModel> Instructor { get; set; }

        public DbSet<StudentModel> Student { get; set; }
        public DbSet<CourseModel> Course { get; set; }
        public DbSet<TurmaModel> Class { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).GetType().Assembly);
            modelBuilder.Entity<PartnnerLevelModel>().HasData(
                new PartnnerLevelModel { Id = 1, Level = Constants.ParceiroCargos.ADMINISTRATIVO },
                new PartnnerLevelModel { Id = 2, Level = Constants.ParceiroCargos.ANALISTA },
                new PartnnerLevelModel { Id = 3, Level = Constants.ParceiroCargos.EMPRESA}
            );
            modelBuilder.Entity<EdrivingLevelModel>().HasData(
                new EdrivingLevelModel { Id = 1, Level = Constants.EdrivingCargos.ADMINISTRATIVO },
                new EdrivingLevelModel { Id = 2, Level = Constants.EdrivingCargos.ANALISTA },
                new EdrivingLevelModel { Id = 3, Level = Constants.EdrivingCargos.DIRETOR }
            );
        }

    }
}
