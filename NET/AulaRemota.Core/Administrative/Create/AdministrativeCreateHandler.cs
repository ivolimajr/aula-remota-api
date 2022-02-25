using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.Create
{
    class AdministrativeCreateHandler : IRequestHandler<AdministrativeCreateInput, AdministrativeModel>
    {
        private readonly IRepository<DrivingSchoolModel, int> _drivingSchoolRepository;
        private readonly IRepository<UserModel, int> _userRepository;
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;
        private readonly IMediator _mediator;

        public AdministrativeCreateHandler(IRepository<DrivingSchoolModel, int> drivingSchoolRepository,
            IRepository<UserModel, int> userRepository,
            IRepository<AdministrativeModel, int> administrativeRepository,
            IMediator mediator)
        {
            _drivingSchoolRepository = drivingSchoolRepository;
            _userRepository = userRepository;
            _administrativeRepository = administrativeRepository;
            _mediator = mediator;
        }

        public Task<AdministrativeModel> Handle(AdministrativeCreateInput request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
