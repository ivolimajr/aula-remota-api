using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
