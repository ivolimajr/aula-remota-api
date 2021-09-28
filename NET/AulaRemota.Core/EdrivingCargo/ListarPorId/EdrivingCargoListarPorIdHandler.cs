using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.EdrivingCargo.ListarPorId
{
    public class EdrivingCargoListarPorIdHandler : IRequestHandler<EdrivingCargoListarPorIdInput, EdrivingCargoListarPorIdResponse>
    {
        private readonly IRepository<EdrivingCargoModel> _edrivingCargoRepository;

        public EdrivingCargoListarPorIdHandler(IRepository<EdrivingCargoModel> edrivingCargoRepository)
        {
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        public async Task<EdrivingCargoListarPorIdResponse> Handle(EdrivingCargoListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var res = await _edrivingCargoRepository.GetByIdAsync(request.Id);
                if (res == null) throw new HttpClientCustomException("Não Encontrado");

                var result = await _edrivingCargoRepository.Context
                        .Set<EdrivingCargoModel>()
                        .Where(u => u.Id == request.Id)
                        .FirstAsync();

                return new EdrivingCargoListarPorIdResponse { Item = result };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
