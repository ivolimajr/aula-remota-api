using AulaRemota.Api.Models.Requests;
using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IParceiroServices
    {
        Parceiro Create(ParceiroCreateRequest entity);

        Parceiro Update(ParceiroCreateRequest entity);

        IEnumerable<Parceiro> GetAll();
        IEnumerable<Parceiro> GetAllWithRelationship();

        Parceiro GetById(int id);

        IEnumerable<Parceiro> GetWhere(Func<Parceiro, bool> predicado);

        void Delete(int id);

        bool Inativar(int id);

        bool Ativar(int id);
        bool ValidateEntity(ParceiroCreateRequest entity);
    }
}
