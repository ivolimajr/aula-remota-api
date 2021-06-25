using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;
using System;
using System.Linq;

namespace AulaRemota.Infra.Repository
{
    public class EdrivingCargoRepository : EFRepository<EdrivingCargo>, IEdrivingCargoRepository
    {
        public EdrivingCargoRepository(SqlContext context) : base(context)
        {

        }
    }
}
