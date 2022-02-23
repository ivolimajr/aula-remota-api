using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Partnner.GetAll
{
    public class GetAllPartnnerHandler : IRequestHandler<GetAllPartnnerInput, List<PartnnerModel>>
    {
        private readonly IRepository<PartnnerModel, int> _parceiroRepository;

        public GetAllPartnnerHandler(IRepository<PartnnerModel, int> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<List<PartnnerModel>> Handle(GetAllPartnnerInput request, CancellationToken cancellationToken)
        {
            try
            {
                return await _parceiroRepository.Context
                    .Set<PartnnerModel>()
                    .Include(e => e.User)
                    .Include(e => e.Level)
                    .Include(e => e.Address)
                    .Include(e => e.PhonesNumbers)
                    .Where(e => e.User.Status > 0)
                    .ToListAsync(); ;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(GetAllPartnnerInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
