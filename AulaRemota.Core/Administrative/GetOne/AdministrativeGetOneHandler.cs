using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.GetOne
{
    public class AdministrativeGetOneHandler : IRequestHandler<AdministrativeGetOneInput, AdministrativeModel>
    {
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;

        public AdministrativeGetOneHandler(IRepository<AdministrativeModel, int> administrativeRepository)
            => _administrativeRepository = administrativeRepository;

        public async Task<AdministrativeModel> Handle(AdministrativeGetOneInput request, CancellationToken cancellationToken)
        {
            return await _administrativeRepository.Where(e => e.Id.Equals(request.Id))
                                .Include(e => e.Address)
                                .Include(e => e.PhonesNumbers)
                                .FirstOrDefaultAsync();
        }
    }
}
