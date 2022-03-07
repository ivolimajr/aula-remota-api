using AulaRemota.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace AulaRemota.Infra.Repository
{
    public class Repository<TEntity, TKey> : IDisposable, IRepository<TEntity, TKey> where TEntity : class
    {
        private MySqlContext _context;
        private IDbContextTransaction _transaction;
        public virtual DbSet<TEntity> Model
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }
        public Repository(MySqlContext dbContext)
        {
            _context = dbContext;
        }

        public DbContext Context
        {
            get { return this._context; }
        }

        //INSERIR
        public virtual TEntity Add(TEntity entity)
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Added;
            var result = Model.Add(entity);
            return result.Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await Model.AddAsync(entity);
            return result.Entity;
        }

        //ATUALIZAR
        public virtual void Update(TEntity entity)
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.Update(entity);
        }

        //BUSCAR COM CLÁUSULA
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = Model.AsNoTracking<TEntity>().IgnoreAutoIncludes();
            return query.Where(filter);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().AsNoTracking().Where(filter).FirstOrDefault();
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return Model.AsNoTracking<TEntity>().Any(filter);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(filter).FirstOrDefaultAsync();
        }

        //REMOVER
        public virtual void Delete(TEntity entity)
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Remove(entity);
        }

        //BUSCAR TODOS
        public virtual IEnumerable<TEntity> All()
        {
            return Model.AsNoTracking().IgnoreAutoIncludes().AsEnumerable();
        }

        //BUSCAR POR ID
        public virtual TEntity Find(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> FindAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
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