using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ParceiroCargo.ListarPorId
{
    public class ParceiroCargoListarPorIdHandler : IRequestHandler<ParceiroCargoListarPorIdInput, ParceiroCargoListarPorIdResponse>
    {
        private readonly IRepository<ParceiroCargoModel> _edrivingCargoRepository;

        public ParceiroCargoListarPorIdHandler(IRepository<ParceiroCargoModel> edrivingCargoRepository)
        {
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        public async Task<ParceiroCargoListarPorIdResponse> Handle(ParceiroCargoListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                var res = await _edrivingCargoRepository.GetByIdAsync(request.Id);
                if (res == null) throw new HttpClientCustomException("Não Encontrado");

                var result = await _edrivingCargoRepository.Context
                        .Set<ParceiroCargoModel>()
                        .Where(u => u.Id == request.Id)
                        .FirstAsync();

                return new ParceiroCargoListarPorIdResponse { Item = result };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
