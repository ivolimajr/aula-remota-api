using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.Repository.UnitOfWorkConfig
{
    public interface IUnitOfWorkFactory<TContext>
         where TContext : DbContext, new()
    {
        IUnitOfWork<TContext> Create();
    }
}
