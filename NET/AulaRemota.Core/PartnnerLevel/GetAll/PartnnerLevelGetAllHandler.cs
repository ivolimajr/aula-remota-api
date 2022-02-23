using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.PartnnerLevel.GetAll
{
    public class PartnnerLevelGetAllHandler : IRequestHandler<PartnnerLevelGetAllInput, List<PartnnerLevelModel>>
    {
        private readonly IRepository<PartnnerLevelModel, int>_edrivingCargoRepository;

        public PartnnerLevelGetAllHandler(IRepository<PartnnerLevelModel, int>edrivingRepository)
        {
            _edrivingCargoRepository = edrivingRepository;
        }

        public async Task<List<PartnnerLevelModel>> Handle(PartnnerLevelGetAllInput request, CancellationToken cancellationToken)
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
                    ModelName = nameof(PartnnerLevelGetAllHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}