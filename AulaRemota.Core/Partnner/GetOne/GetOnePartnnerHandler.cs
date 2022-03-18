using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System;

namespace AulaRemota.Core.Partnner.GetOne
{
    public class GetOnePartnnerHandler : IRequestHandler<GetOnePartnnerInput, GetOnePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel, int> _partnnerRepository;

        public GetOnePartnnerHandler(IRepository<PartnnerModel, int> partnnerRepository) => _partnnerRepository = partnnerRepository;

        public async Task<GetOnePartnnerResponse> Handle(GetOnePartnnerInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var partnnerEntity = _partnnerRepository
                        .Where(e => e.User.Status > 0 && e.Id == request.Id)
                        .Include(e => e.User)
                        .Include(e => e.Level)
                        .Include(e => e.Address)
                        .Include(e => e.PhonesNumbers)                        
                        .FirstOrDefault();
                if (partnnerEntity == null) throw new CustomException("Não Encontrado");

                return new GetOnePartnnerResponse
                {
                    Id = partnnerEntity.Id,
                    Name = partnnerEntity.Name,
                    Email = partnnerEntity.Email,
                    PhonesNumbers = partnnerEntity.PhonesNumbers,
                    Description = partnnerEntity.Description,
                    Cnpj = partnnerEntity.Cnpj,
                    LevelId = partnnerEntity.LevelId,
                    Level = partnnerEntity.Level,
                    AddressId = partnnerEntity.AddressId,
                    Address = partnnerEntity.Address,
                    UserId = partnnerEntity.UserId,
                    User = partnnerEntity.User,
                };
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(GetOnePartnnerInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }
}
