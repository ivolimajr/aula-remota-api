using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.Partnner.GetOne
{
    public class GetOnePartnnerHandler : IRequestHandler<GetOnePartnnerInput, GetOnePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel> _parceiroRepository;

        public GetOnePartnnerHandler(IRepository<PartnnerModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<GetOnePartnnerResponse> Handle(GetOnePartnnerInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var res = await _parceiroRepository.GetByIdAsync(request.Id);
                if (res == null) throw new CustomException("Não Encontrado");

                var result = await _parceiroRepository.Context
                        .Set<PartnnerModel>()
                        .Include(e => e.User)
                        .Include(e => e.Level)
                        .Include(e => e.Address)
                        .Include(e => e.PhonesNumbers)
                        .Where(e => e.Id == request.Id)
                        .Where(e => e.User.Status > 0)
                        .FirstAsync();

                return new GetOnePartnnerResponse {
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
