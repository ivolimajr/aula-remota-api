using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Partnner.GetAll
{
    public class GetAllPartnnerHandler : IRequestHandler<GetAllPartnnerInput, List<PartnnerModel>>
    {
        private readonly IRepository<PartnnerModel> _parceiroRepository;

        public GetAllPartnnerHandler(IRepository<PartnnerModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<List<PartnnerModel>> Handle(GetAllPartnnerInput request, CancellationToken cancellationToken)
        {
            try
            {
                return await _parceiroRepository.Context
                    .Set<PartnnerModel>()
                    .Include(e => e.Usuario)
                    .Include(e => e.Cargo)
                    .Include(e => e.Endereco)
                    .Include(e => e.Telefones)
                    .Where(e => e.Usuario.status > 0)
                    .ToListAsync(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
