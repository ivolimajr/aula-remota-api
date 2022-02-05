using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.EdrivingCargo.ListarPorId
{
    public class EdrivingCargoListarPorIdHandler : IRequestHandler<EdrivingCargoListarPorIdInput, EdrivingCargoModel>
    {
        private readonly IRepository<EdrivingCargoModel> _edrivingCargoRepository;

        public EdrivingCargoListarPorIdHandler(IRepository<EdrivingCargoModel> edrivingCargoRepository)
        {
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        public async Task<EdrivingCargoModel> Handle(EdrivingCargoListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var result = await _edrivingCargoRepository.GetByIdAsync(request.Id);
                if (result == null) throw new HttpClientCustomException("Não Encontrado");

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
