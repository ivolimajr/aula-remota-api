using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Infra.UnitOfWork
{
    public interface IUnitOfWorkFactory<TContext>
         where TContext : DbContext, new()
    {
        IUnitOfWork<TContext> Create();
    }
}

