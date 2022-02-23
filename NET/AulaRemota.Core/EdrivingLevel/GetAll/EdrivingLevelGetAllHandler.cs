using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.EdrivingLevel.GetAll
{
    public class EdrivingLevelGetAllHandler : IRequestHandler<EdrivingLevelGetAllInput, List<EdrivingLevelModel>>
    {
        private readonly IRepository<EdrivingLevelModel, int> _edrivingCargoRepository;

        public EdrivingLevelGetAllHandler(IRepository<EdrivingLevelModel, int> edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<List<EdrivingLevelModel>> Handle(EdrivingLevelGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                return _edrivingCargoRepository.All().OrderBy(u => u.Level).ToList();
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingLevelGetAllInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}