using AulaRemota.Core.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IEdrivingRepository : IRepository<Edriving>
    {

        IEnumerable<Edriving> GetAllWithRelationship();
        Edriving GetFullObjectById(int id);

    }
}
