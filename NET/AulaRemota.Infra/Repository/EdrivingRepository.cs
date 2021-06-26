using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AulaRemota.Infra.Repository
{
    public class EdrivingRepository : EFRepository<Edriving>, IEdrivingRepository
    {
        public EdrivingRepository(MySqlContext context) : base(context)
        {

        }

        Edriving IEdrivingRepository.GetFullObjectById(int id)
        {
            return _context.Set<Edriving>().Include(c => c.Cargo).Include(u => u.Usuario).Where( e => e.Id == id).FirstOrDefault();
        }

        IEnumerable<Edriving> IEdrivingRepository.GetAllWithRelationship()
        {
            return _context.Set<Edriving>().Include(c => c.Cargo).Include(u => u.Usuario).Where(e => e.Usuario.status >= 1).AsEnumerable();
        }
    }
}
