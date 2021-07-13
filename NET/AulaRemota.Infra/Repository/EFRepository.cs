﻿using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AulaRemota.Infra.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MySqlContext _context;

        public EFRepository(MySqlContext dbContext)
        {
            _context = dbContext;
        }

        public DbContext Context
        {
            get { return this._context; }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        //INSERIR
        TEntity IRepository<TEntity>.Create(TEntity entity)
        {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
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

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        //BUSCAR COM CLÁUSULA
        IEnumerable<TEntity> IRepository<TEntity>.GetWhere(Func<TEntity, bool> queryLambda)
        {
            return _context.Set<TEntity>().Where(queryLambda).AsEnumerable();
        }

        public TEntity Find(Func<TEntity, bool> queryLambda)
        {
            return _context.Set<TEntity>().Where(queryLambda).FirstOrDefault();
        }

        //REMOVER
        void IRepository<TEntity>.Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
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
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

    }
}
