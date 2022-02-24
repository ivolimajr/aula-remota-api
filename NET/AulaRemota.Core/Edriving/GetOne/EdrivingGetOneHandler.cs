using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AulaRemota.Core.Edriving.GetOne
{
    public class EdrivingGetOneHandler : IRequestHandler<EdrivingGetOneInput, EdrivingModel>
    {
        private readonly IRepository<EdrivingModel, int> _edrivingRepository;

        public EdrivingGetOneHandler(IRepository<EdrivingModel, int> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<EdrivingModel> Handle(EdrivingGetOneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var res = await _edrivingRepository.FindAsync(request.Id);
                if (res == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                var result = await _edrivingRepository.Context
                        .Set<EdrivingModel>()
                        .Include(e => e.User)
                        .Include(e => e.User.Roles)
                        .Include(e => e.Level)
                        .Include(e => e.PhonesNumbers)
                        .Where(e => e.Id == res.Id)
                        .Where(e => e.User.Status > 0)
                        .FirstOrDefaultAsync();

                return result;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingGetOneInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
