using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;

namespace AulaRemota.Infra.Repository
{
    public class ParceiroCargoRepository : Repository<ParceiroCargo>, IParceiroCargoRepository
    {
        public ParceiroCargoRepository(MySqlContext context) : base(context)
        {

        }
    }
}
