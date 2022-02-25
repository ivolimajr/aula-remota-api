using AulaRemota.Infra.Context;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace AulaRemota.Infra.Repository.UnitOfWorkConfig
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        public MySqlContext Context { get; set; }

        public IRepository<ApiUserModel, int> ApiUser { get; set; }

        public IRepository<AddressModel, int> Address { get; set; }

        public IRepository<EdrivingModel, int> Edriving { get; set; }

        public IRepository<EdrivingLevelModel, int> EdrivingLevel { get; set; }

        public IRepository<FileModel, int> File { get; set; }

        public IRepository<PartnnerModel, int> Partnner { get; set; }

        public IRepository<PartnnerLevelModel, int> PartnnerLevel { get; set; }

        public IRepository<PhoneModel, int> Phone { get; set; }

        public IRepository<RolesModel, int> Roles { get; set; }

        public IRepository<UserModel, int> User { get; set; }

        public IRepository<AdministrativeModel, int> Administrative { get; set; }

        public IRepository<CourseModel, int> Course { get; set; }

        public IRepository<DrivingSchoolModel, int> DrivingSchool { get; set; }

        public IRepository<InstructorModel, int> Instructor { get; set; }

        public IRepository<StudentModel, int> Student { get; set; }

        public IRepository<TurmaModel, int> Turma { get; set; }

        public UnitOfWork(MySqlContext context, DbTransaction transaction = null) => Init(context, transaction);

        private void Init(MySqlContext context, DbTransaction transaction = null)
        {
            Context = context;
            Context.Database.AutoTransactionsEnabled = false;
            Context.ChangeTracker.AutoDetectChangesEnabled = true;
            Context.ChangeTracker.LazyLoadingEnabled = false;

            if (transaction != null)
                Context.Database.UseTransaction(transaction);

            ApiUser = new Repository<ApiUserModel, int>(context);
            Address = new Repository<AddressModel, int>(context);
            Edriving = new Repository<EdrivingModel, int>(context);
            EdrivingLevel = new Repository<EdrivingLevelModel, int>(context);
            File = new Repository<FileModel, int>(context);
            Partnner = new Repository<PartnnerModel, int>(context);
            PartnnerLevel = new Repository<PartnnerLevelModel, int>(context);
            Phone = new Repository<PhoneModel, int>(context);
            Roles = new Repository<RolesModel, int>(context);
            User = new Repository<UserModel, int>(context);
            Administrative = new Repository<AdministrativeModel, int>(context);
            Course = new Repository<CourseModel, int>(context);
            DrivingSchool = new Repository<DrivingSchoolModel, int>(context);
            Instructor = new Repository<InstructorModel, int>(context);
            Student = new Repository<StudentModel, int>(context);
            ApiUser = new Repository<ApiUserModel, int>(context);
            Turma = new Repository<TurmaModel, int>(context);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return Context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
