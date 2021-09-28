using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}