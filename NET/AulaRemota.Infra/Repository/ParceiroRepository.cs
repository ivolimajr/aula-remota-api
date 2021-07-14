using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AulaRemota.Infra.Repository
{
    public class ParceiroRepository : Repository<Parceiro>, IParceiroRepository
    {
        public ParceiroRepository(MySqlContext context) : base(context)
        {

        }

        Parceiro IParceiroRepository.GetFullObjectById(int id)
        {
            return _context.Set<Parceiro>().Include(c => c.Cargo).Include(u => u.Usuario).Where( e => e.Id == id).FirstOrDefault();
        }

        IEnumerable<Parceiro> IParceiroRepository.GetAllWithRelationship()
        {
            return _context.Set<Parceiro>().Include(c => c.Cargo).Include(u => u.Usuario).Include(u => u.Endereco).Where(e => e.Usuario.status >= 1).AsEnumerable();
        }
    }
}
