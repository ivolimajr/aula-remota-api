using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AulaRemota.Infra.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        public DbContext Context { get; }

        public TEntity Add(TEntity entity);

        public Task<TEntity> AddAsync(TEntity entity);

        public void Update(TEntity entity);

        public IEnumerable<TEntity> All();

        public TEntity Find(int id);
        public Task<TEntity> FindAsync(int id);

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> queryLambda);

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> queryLambda);
        public bool Exists(Expression<Func<TEntity, bool>> queryLambda);

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> queryLambda);

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
        public void SaveChanges() => Context.SaveChanges();

        public void Delete(TEntity entity);
        public void CreateTransaction();
        public void Commit();
        public void Rollback();
        public void Save();
    }
}