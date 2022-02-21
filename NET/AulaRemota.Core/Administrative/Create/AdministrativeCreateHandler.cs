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
        private readonly IRepository<DrivingSchoolModel> _drivingSchoolRepository;
        private readonly IRepository<UserModel> _userRepository;
        private readonly IRepository<AdministrativeModel> _administrativeRepository;
        private readonly IMediator _mediator;

        public AdministrativeCreateHandler(IRepository<DrivingSchoolModel> drivingSchoolRepository, 
            IRepository<UserModel> userRepository, 
            IRepository<AdministrativeModel> administrativeRepository, 
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
