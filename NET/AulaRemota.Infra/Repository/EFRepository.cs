using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AulaRemota.Infra.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MySqlContext _context;

        public EFRepository(MySqlContext dbContext)
        {
            _context = dbContext;
        }


        //INSERIR
        TEntity IRepository<TEntity>.Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        //ATUALIZAR
        TEntity IRepository<TEntity>.Update(TEntity entity)
        {
            var result = _context.Set<TEntity>().FirstOrDefault(p => p.Equals(entity));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        //BUSCAR COM CLÁUSULA
        IEnumerable<TEntity> IRepository<TEntity>.GetWhere(Expression<Func<TEntity, bool>> predicado)
        {
            return _context.Set<TEntity>().Where(predicado).AsEnumerable();
        }

        //REMOVER
        bool IRepository<TEntity>.Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //BUSCAR TODOS
        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }

        //BUSCAR POR ID
        TEntity IRepository<TEntity>.GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
    }
}
