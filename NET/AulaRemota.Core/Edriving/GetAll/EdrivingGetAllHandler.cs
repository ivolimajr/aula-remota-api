using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.GetAll
{
    public class EdrivingGetAllHandler : IRequestHandler<EdrivingGetAllInput, List<EdrivingModel>>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;

        public EdrivingGetAllHandler(IRepository<EdrivingModel> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<List<EdrivingModel>> Handle(EdrivingGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                return await _edrivingRepository.Context
                    .Set<EdrivingModel>()
                    .Include(u => u.User)
                    .Include(c => c.Level)
                    .Include(t => t.PhonesNumbers)
                    .ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
