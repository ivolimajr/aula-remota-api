using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IParceiroCargoServices
    {
        IEnumerable<ParceiroCargo> GetAll();
        
        ParceiroCargo GetById(int id);
        
        ParceiroCargo Create(ParceiroCargo entity);

        ParceiroCargo Update(ParceiroCargo entity);

        IEnumerable<ParceiroCargo> GetWhere(Expression<Func<ParceiroCargo, bool>> predicado);

        void Delete(int id);
    }
}
