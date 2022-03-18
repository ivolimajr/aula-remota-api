using AulaRemota.Infra.Context;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace AulaRemota.Infra.Repository.UnitOfWorkConfig
{
    public interface IUnitOfWork
    {
        MySqlContext Context { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();

        IRepository<ApiUserModel, int> ApiUser { get; }
        IRepository<AddressModel, int> Address { get; }
        IRepository<EdrivingModel, int> Edriving { get; }
        IRepository<EdrivingLevelModel, int> EdrivingLevel { get; }
        IRepository<FileModel, int> File { get; }
        IRepository<PartnnerModel, int> Partnner { get; }
        IRepository<PartnnerLevelModel, int> PartnnerLevel { get; }
        IRepository<PhoneModel, int> Phone { get; }
        IRepository<RolesModel, int> Roles { get; }
        IRepository<UserModel, int> User { get; }
        IRepository<AdministrativeModel, int> Administrative { get; }
        IRepository<CourseModel, int> Course { get; }
        IRepository<DrivingSchoolModel, int> DrivingSchool { get; }
        IRepository<InstructorModel, int> Instructor { get; }
        IRepository<StudentModel, int> Student { get; }
        IRepository<TurmaModel, int> Turma { get; }

    }
}
