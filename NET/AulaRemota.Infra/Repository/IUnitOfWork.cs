using System.Threading.Tasks;

namespace AulaRemota.Infra.Repository
{
    public interface IUnitOfWork
    {
        public Task<bool>Commit();
        public Task Rollback();
    }
}