using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IParceiroCargoServices
    {
        IEnumerable<ParceiroCargo> GetAll();
        
        ParceiroCargo GetById(int id);
        
        ParceiroCargo Create(ParceiroCargo entity);

        ParceiroCargo Update(ParceiroCargo entity);

        IEnumerable<ParceiroCargo> GetWhere(Func<ParceiroCargo, bool> predicado);

        void Delete(int id);
    }
}
