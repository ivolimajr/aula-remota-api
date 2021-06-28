using AulaRemota.Api.Models.Requests;
using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IEdrivingServices
    {
        Edriving Create(EdrivingCreateRequest entity);

        Edriving Update(EdrivingCreateRequest entity);

        IEnumerable<EdrivingGetAllRequest> GetAll();
        IEnumerable<Edriving> GetAllWithRelationship();

        Edriving GetById(int id);

        IEnumerable<Edriving> GetWhere(Expression<Func<Edriving, bool>> predicado);

        bool Delete(int id);

        bool Inativar(int id);

        bool Ativar(int id);
        bool ValidateEntity(EdrivingCreateRequest entity);
    }
}
