using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AulaRemota.Core.PartnnerLevel.GetOne
{
    public class PartnnerLevelGetOneHandler : IRequestHandler<PartnnerLevelGetOneInput, PartnnerLevelModel>
    {
        private readonly IRepository<PartnnerLevelModel, int>_edrivingCargoRepository;

        public PartnnerLevelGetOneHandler(IRepository<PartnnerLevelModel, int>edrivingCargoRepository)
        {
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        public async Task<PartnnerLevelModel> Handle(PartnnerLevelGetOneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var result = await _edrivingCargoRepository.FindAsync(request.Id);
                if (result == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                return result;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(PartnnerLevelGetOneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }

        }
    }
}
