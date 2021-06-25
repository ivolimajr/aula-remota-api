using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IEdrivingCargoServices
    {
        EdrivingCargo Create(EdrivingCargo entity);

        EdrivingCargo Update(EdrivingCargo entity);

        IEnumerable<EdrivingCargo> GetAll();

        EdrivingCargo GetById(int id);

        IEnumerable<EdrivingCargo> GetWhere(Expression<Func<EdrivingCargo, bool>> predicado);

        bool Delete(int id);
    }
}
