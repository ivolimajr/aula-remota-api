using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.EdrivingCargo.ListarTodos
{
    public class EdrivingCargoListarTodosHandler : IRequestHandler<EdrivingCargoListarTodosInput, List<EdrivingCargoModel>>
    {
        private readonly IRepository<EdrivingCargoModel> _edrivingCargoRepository;

        public EdrivingCargoListarTodosHandler(IRepository<EdrivingCargoModel> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<List<EdrivingCargoModel>> Handle(EdrivingCargoListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                return _edrivingCargoRepository.GetAll().OrderBy(u => u.Cargo).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}