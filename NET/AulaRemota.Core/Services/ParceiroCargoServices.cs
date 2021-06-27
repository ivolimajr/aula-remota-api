using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Services
{
    public class ParceiroCargoServices : IParceiroCargoServices
    {
        private readonly IParceiroCargoRepository _parceiroCargoRepository;

        public ParceiroCargoServices(IParceiroCargoRepository parceiroCargoRepository)
        {
            _parceiroCargoRepository = parceiroCargoRepository;
        }

        ParceiroCargo IParceiroCargoServices.Create(ParceiroCargo entity)
        {
            entity.Cargo = entity.Cargo.ToUpper();
            return _parceiroCargoRepository.Create(entity);
        }

        bool IParceiroCargoServices.Delete(int id)
        {
            var result = _parceiroCargoRepository.GetById(id);
            return _parceiroCargoRepository.Delete(result);
        }

        IEnumerable<ParceiroCargo> IParceiroCargoServices.GetAll()
        {
            return _parceiroCargoRepository.GetAll();
        }

        ParceiroCargo IParceiroCargoServices.GetById(int id)
        {
            return _parceiroCargoRepository.GetById(id);
        }

        IEnumerable<ParceiroCargo> IParceiroCargoServices.GetWhere(Expression<Func<ParceiroCargo, bool>> predicado)
        {
            return _parceiroCargoRepository.GetWhere(predicado);
        }

        ParceiroCargo IParceiroCargoServices.Update(ParceiroCargo entity)
        {
            entity.Cargo = entity.Cargo.ToUpper();
            return _parceiroCargoRepository.Update(entity);
        }
    }
}
