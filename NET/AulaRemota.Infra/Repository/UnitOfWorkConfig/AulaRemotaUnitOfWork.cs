using AulaRemota.Infra.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace AulaRemota.Infra.Repository.UnitOfWorkConfig
{
    public class AulaRemotaUnitOfWork : IUnitOfWork<MySqlContext>, IDisposable
    {
        public MySqlContext Context { get; private set; }

        private IDbContextTransaction _transaction;

        public AulaRemotaUnitOfWork(MySqlContext context)
        {
            Context = context;
            context.ChangeTracker.LazyLoadingEnabled = false;
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
            else
            {
                throw new ArgumentNullException("Context é null");
            }
            GC.SuppressFinalize(this);
        }

        public void CreateTransaction()
        {
            if (Context != null)
            {
                _transaction = Context.Database.BeginTransaction();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }

        public void Commit()
        {
            if (Context != null)
            {
                _transaction.Commit();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }

        public void Rollback()
        {
            if (Context != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }

        public void Save()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }

    }
}
