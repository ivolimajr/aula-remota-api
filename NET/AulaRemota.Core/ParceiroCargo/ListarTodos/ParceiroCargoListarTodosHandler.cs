using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ParceiroCargo.ListarTodos
{
    public class ParceiroCargoListarTodosHandler : IRequestHandler<ParceiroCargoListarTodosInput, List<ParceiroCargoModel>>
    {
        private readonly IRepository<ParceiroCargoModel> _edrivingCargoRepository;

        public ParceiroCargoListarTodosHandler(IRepository<ParceiroCargoModel> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<List<ParceiroCargoModel>> Handle(ParceiroCargoListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {   
                return _edrivingCargoRepository.GetAll().OrderBy(u => u.Cargo).ToList(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}