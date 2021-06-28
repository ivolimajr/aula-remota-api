using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> GetAll();
        
        TEntity GetById(int id);

        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicado);

        bool Delete(TEntity entity);
    }
}
