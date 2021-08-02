using Microsoft.EntityFrameworkCore;
using System;

namespace AulaRemota.Infra.Repository.UnitOfWorkConfig
{
    public interface IUnitOfWork<out TContext> : IDisposable
        where TContext : DbContext, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}