using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Partnner.GetAll
{
    public class GetAllPartnnerHandler : IRequestHandler<GetAllPartnnerInput, List<PartnnerModel>>
    {
        private readonly IRepository<PartnnerModel, int> _partnnerRepository;

        public GetAllPartnnerHandler(IRepository<PartnnerModel, int> partnnerRepository) =>
            _partnnerRepository = partnnerRepository;

        public async Task<List<PartnnerModel>> Handle(GetAllPartnnerInput request, CancellationToken cancellationToken)
        {
            try
            {
                return await _partnnerRepository
                    .Where(e => e.User.Status > 0)
                    .Include(e => e.User)
                    .Include(e => e.Level)
                    .Include(e => e.Address)
                    .Include(e => e.PhonesNumbers)
                    .ToListAsync(); ;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(GetAllPartnnerInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }
}
