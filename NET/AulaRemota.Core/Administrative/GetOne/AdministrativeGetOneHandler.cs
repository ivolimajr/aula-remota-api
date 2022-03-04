using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.GetOne
{
    public class AdministrativeGetOneHandler : IRequestHandler<AdministrativeGetOneInput, AdministrativeModel>
    {
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;

        public AdministrativeGetOneHandler(IRepository<AdministrativeModel, int> administrativeRepository)
        {
            _administrativeRepository = administrativeRepository;
        }

        public Task<AdministrativeModel> Handle(AdministrativeGetOneInput request, CancellationToken cancellationToken)
        {
            return _administrativeRepository.FindAsync(request.Id);
        }
    }
}
