using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IEdrivingRepository : IRepository<Edriving>
    {

        IEnumerable<Edriving> GetAllWithRelationship();
        Edriving GetFullObjectById(int id);

    }
}
