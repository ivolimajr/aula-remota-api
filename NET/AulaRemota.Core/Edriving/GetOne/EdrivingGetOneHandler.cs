using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net;

namespace AulaRemota.Core.Edriving.GetOne
{
    public class EdrivingGetOneHandler : IRequestHandler<EdrivingGetOneInput, EdrivingGetOneResponse>
    {
        private readonly IRepository<EdrivingModel, int> _edrivingRepository;

        public EdrivingGetOneHandler(IRepository<EdrivingModel, int> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<EdrivingGetOneResponse> Handle(EdrivingGetOneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var res = await _edrivingRepository.FindAsync(request.Id);
                if (res == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                var result = await _edrivingRepository.Context
                        .Set<EdrivingModel>()
                        .Include(e => e.User)
                        .Include(e => e.Level)
                        .Include(e => e.PhonesNumbers)
                        .Where(e => e.Id == res.Id)
                        .Where(e => e.User.Status > 0)
                        .FirstOrDefaultAsync();

                return new EdrivingGetOneResponse { 
                
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    Cpf = result.Cpf,
                    PhonesNumbers = result.PhonesNumbers,
                    LevelId = result.LevelId,
                    Level = result.Level,
                    UserId= result.UserId,
                    User= result.User
                };
            }
            catch (CustomException e)
            {
                _edrivingRepository.Rollback();
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
