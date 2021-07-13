using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.ListarTodos
{
    public class EdrivingListarPorIdHandler : IRequestHandler<EdrivingListarPorIdInput, EdrivingListarPorIdResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;

        public EdrivingListarPorIdHandler(IRepository<EdrivingModel> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<EdrivingListarPorIdResponse> Handle(EdrivingListarPorIdInput request, CancellationToken cancellationToken)
        {
            var result = await _edrivingRepository.Context
                    .Set<EdrivingModel>()
                    .Include(u => u.Usuario)
                    .Include(c => c.Cargo)
                    .Where( u => u.Usuario.status > 0)
                    .FirstAsync();

            return new EdrivingListarPorIdResponse { Item = result };
        }
    }
}
