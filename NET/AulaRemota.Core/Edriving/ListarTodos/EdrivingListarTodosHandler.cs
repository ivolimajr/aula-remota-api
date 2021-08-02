using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.ListarTodos
{
    public class EdrivingListarTodosHandler : IRequestHandler<EdrivingListarTodosInput, EdrivingListarTodosResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;

        public EdrivingListarTodosHandler(IRepository<EdrivingModel> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<EdrivingListarTodosResponse> Handle(EdrivingListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _edrivingRepository.Context
                    .Set<EdrivingModel>()
                    .Include(u => u.Usuario)
                    .Include(c => c.Cargo)
                    .Include(t => t.Telefones)
                    //.Where(u => u.Usuario.status > 0)
                    .OrderBy(e => e.Id).ToListAsync();

                return new EdrivingListarTodosResponse { Items = result };
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
