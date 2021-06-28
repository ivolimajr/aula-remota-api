using AulaRemota.Core.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IParceiroRepository : IRepository<Parceiro>
    {

        IEnumerable<Parceiro> GetAllWithRelationship();
        Parceiro GetFullObjectById(int id);

    }
}
