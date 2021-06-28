using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;

namespace AulaRemota.Infra.Repository
{
    public class EnderecoRepository : EFRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MySqlContext context) : base(context)
        {

        }
    }
}
