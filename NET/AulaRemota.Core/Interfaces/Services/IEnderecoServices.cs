using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IEnderecoServices
    {
        IEnumerable<Endereco> GetAll();
        
        Endereco GetById(int id);
        
        Endereco Create(Endereco entity);

        Endereco Update(Endereco entity);

        IEnumerable<Endereco> GetWhere(Func<Endereco, bool> predicado);

        void Delete(int id);
    }
}
