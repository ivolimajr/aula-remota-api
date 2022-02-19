using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.EdrivingLevel.GetAll
{
    public class EdrivingLevelGetAllHandler : IRequestHandler<EdrivingLevelGetAllInput, List<EdrivingLevelModel>>
    {
        private readonly IRepository<EdrivingLevelModel> _edrivingCargoRepository;

        public EdrivingLevelGetAllHandler(IRepository<EdrivingLevelModel> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<List<EdrivingLevelModel>> Handle(EdrivingLevelGetAllInput request, CancellationToken cancellationToken)
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