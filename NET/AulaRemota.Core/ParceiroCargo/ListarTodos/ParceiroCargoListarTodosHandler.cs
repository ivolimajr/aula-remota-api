using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ParceiroCargo.ListarTodos
{
    public class ParceiroCargoListarTodosHandler : IRequestHandler<ParceiroCargoListarTodosInput, ParceiroCargoListarTodosResponse>
    {
        private readonly IRepository<ParceiroCargoModel> _edrivingCargoRepository;

        public ParceiroCargoListarTodosHandler(IRepository<ParceiroCargoModel> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<ParceiroCargoListarTodosResponse> Handle(ParceiroCargoListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _edrivingCargoRepository.Context.Set<ParceiroCargoModel>()
                    .OrderBy(u => u.Cargo).ToListAsync();

                return new ParceiroCargoListarTodosResponse { Items = result };
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}