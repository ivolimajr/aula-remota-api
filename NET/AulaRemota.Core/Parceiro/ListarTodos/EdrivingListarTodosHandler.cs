using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Parceiro.ListarTodos
{
    public class ParceiroListarTodosHandler : IRequestHandler<ParceiroListarTodosInput, ParceiroListarTodosResponse>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;

        public ParceiroListarTodosHandler(IRepository<ParceiroModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<ParceiroListarTodosResponse> Handle(ParceiroListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _parceiroRepository.Context
                    .Set<ParceiroModel>()
                    .Include(u => u.Usuario)
                    .Include(c => c.Cargo)
                    .Include(e => e.Endereco)
                    .Include(t => t.Telefones)
                    .Where(u => u.Usuario.status > 0)
                    .OrderBy(e => e.Id).ToListAsync();

                return new ParceiroListarTodosResponse { Items = result };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
