using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.EdrivingCargo.ListarTodos
{
    public class EdrivingCargoListarTodosHandler : IRequestHandler<EdrivingCargoListarTodosInput, EdrivingCargoListarTodosResponse>
    {
        private readonly IRepository<EdrivingCargoModel> _edrivingCargoRepository;

        public EdrivingCargoListarTodosHandler(IRepository<EdrivingCargoModel> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<EdrivingCargoListarTodosResponse> Handle(EdrivingCargoListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _edrivingCargoRepository.Context.Set<EdrivingCargoModel>()
                    .OrderBy(u => u.Cargo).ToListAsync();

                return new EdrivingCargoListarTodosResponse { Items = result };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
