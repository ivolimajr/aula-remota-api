using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AulaRemota.Core.Partnner.GetOne
{
    public class GetOnePartnnerHandler : IRequestHandler<GetOnePartnnerInput, GetOnePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel, int> _partnnerRepository;

        public GetOnePartnnerHandler(IRepository<PartnnerModel, int> partnnerRepository)
        {
            _partnnerRepository = partnnerRepository;
        }

        public async Task<GetOnePartnnerResponse> Handle(GetOnePartnnerInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                bool res = _partnnerRepository.Exists(e => e.Id == request.Id);
                if (!res) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                var result = await _partnnerRepository.Context
                        .Set<PartnnerModel>()
                        .Include(e => e.User)
                        .Include(e => e.Level)
                        .Include(e => e.Address)
                        .Include(e => e.PhonesNumbers)
                        .Where(e => e.Id == request.Id)
                        .Where(e => e.User.Status > 0)
                        .FirstAsync();

                return new GetOnePartnnerResponse
                {
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    PhonesNumbers = result.PhonesNumbers,
                    Description = result.Description,
                    Cnpj = result.Cnpj,
                    LevelId = result.LevelId,
                    Level = result.Level,
                    AddressId = result.AddressId,
                    Address = result.Address,
                    UserId = result.UserId,
                    User = result.User,
                };
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(GetOnePartnnerInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
