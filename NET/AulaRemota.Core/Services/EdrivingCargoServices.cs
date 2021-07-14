using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Services
{
    public class EdrivingCargoServices : IEdrivingCargoServices
    {
        private readonly IEdrivingCargoRepository _edrivingCargoRepository;

        public EdrivingCargoServices(IEdrivingCargoRepository edrivingCargoRepository)
        {
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        EdrivingCargo IEdrivingCargoServices.Create(EdrivingCargo entity)
        {
            entity.Id = 0;
            entity.Cargo = entity.Cargo.ToUpper();
            return _edrivingCargoRepository.Create(entity);
        }

        bool IEdrivingCargoServices.Delete(int id)
        {
            var result = _edrivingCargoRepository.GetById(id);
            _edrivingCargoRepository.Delete(result);
            return true;
        }

        IEnumerable<EdrivingCargo> IEdrivingCargoServices.GetAll()
        {
            return _edrivingCargoRepository.GetAll();
        }

        EdrivingCargo IEdrivingCargoServices.GetById(int id)
        {
            return _edrivingCargoRepository.GetById(id);
        }

        IEnumerable<EdrivingCargo> IEdrivingCargoServices.GetWhere(Expression<Func<EdrivingCargo, bool>> queryLambda)
        {
            return _edrivingCargoRepository.GetWhere(queryLambda);
        }

        EdrivingCargo IEdrivingCargoServices.Update(EdrivingCargo entity)
        {
            entity.Cargo = entity.Cargo.ToUpper();
            return _edrivingCargoRepository.Update(entity);
        }
    }
}
