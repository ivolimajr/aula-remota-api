using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IEdrivingCargoServices
    {
        IEnumerable<EdrivingCargo> GetAll();
        
        EdrivingCargo GetById(int id);
        
        EdrivingCargo Create(EdrivingCargo entity);

        EdrivingCargo Update(EdrivingCargo entity);

        IEnumerable<EdrivingCargo> GetWhere(Func<EdrivingCargo, bool> queryLambda);

        bool Delete(int id);
    }
}
