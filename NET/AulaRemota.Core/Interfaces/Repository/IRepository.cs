using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public DbContext Context { get; }

        TEntity Create(TEntity entity);

        public Task<TEntity> CreateAsync(TEntity entity);

        TEntity Update(TEntity entity);

        public Task<TEntity> UpdateAsync(TEntity entity);

        IEnumerable<TEntity> GetAll();
        
        public TEntity GetById(int id);
        public Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetWhere(Func<TEntity, bool> queryLambda);
        TEntity Find(Func<TEntity, bool> queryLambda);

        void Delete(TEntity entity);
    }
}
