using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public DbContext Context { get; }

        public TEntity Create(TEntity entity);

        public Task<TEntity> CreateAsync(TEntity entity);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> GetAll();
        
        public TEntity GetById(int id);
        public Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> queryLambda);

        TEntity Find(Expression<Func<TEntity, bool>> queryLambda);

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> queryLambda);

        void Delete(TEntity entity);
    }
}
