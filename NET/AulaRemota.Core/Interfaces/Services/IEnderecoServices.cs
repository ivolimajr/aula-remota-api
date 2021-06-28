using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IEnderecoServices
    {
        IEnumerable<Endereco> GetAll();
        
        Endereco GetById(int id);
        
        Endereco Create(Endereco entity);

        Endereco Update(Endereco entity);

        IEnumerable<Endereco> GetWhere(Expression<Func<Endereco, bool>> predicado);

        bool Delete(int id);
    }
}
