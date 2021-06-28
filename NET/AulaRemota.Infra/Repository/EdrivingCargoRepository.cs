﻿using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;

namespace AulaRemota.Infra.Repository
{
    public class EdrivingCargoRepository : EFRepository<EdrivingCargo>, IEdrivingCargoRepository
    {
        public EdrivingCargoRepository(MySqlContext context) : base(context)
        {

        }
    }
}
