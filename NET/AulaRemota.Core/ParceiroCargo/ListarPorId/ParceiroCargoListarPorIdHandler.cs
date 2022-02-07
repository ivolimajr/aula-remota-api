using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.ParceiroCargo.ListarPorId
{
    public class ParceiroCargoListarPorIdHandler : IRequestHandler<ParceiroCargoListarPorIdInput, ParceiroCargoModel>
    {
        private readonly IRepository<ParceiroCargoModel> _edrivingCargoRepository;

        public ParceiroCargoListarPorIdHandler(IRepository<ParceiroCargoModel> edrivingCargoRepository)
        {
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        public async Task<ParceiroCargoModel> Handle(ParceiroCargoListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var result = await _edrivingCargoRepository.GetByIdAsync(request.Id);
                if (result == null) throw new CustomException("Não Encontrado");

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
