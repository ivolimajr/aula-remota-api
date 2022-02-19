using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.PartnnerCargo.ListarTodos
{
    public class ParceiroCargoListarTodosHandler : IRequestHandler<ParceiroCargoListarTodosInput, List<PartnnerLevelModel>>
    {
        private readonly IRepository<PartnnerLevelModel> _edrivingCargoRepository;

        public ParceiroCargoListarTodosHandler(IRepository<PartnnerLevelModel> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<List<PartnnerLevelModel>> Handle(ParceiroCargoListarTodosInput request, CancellationToken cancellationToken)
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