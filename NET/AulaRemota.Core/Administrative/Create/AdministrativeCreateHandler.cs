using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.Create
{
    class AdministrativeCreateHandler : IRequestHandler<AdministrativeCreateInput, AdministrativeModel>
    {
        private readonly IRepository<DrivingSchoolModel, int> _drivingSchoolRepository;
        private readonly IRepository<UserModel, int> _userRepository;
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;

        public AdministrativeCreateHandler(IRepository<DrivingSchoolModel, int> drivingSchoolRepository,
            IRepository<UserModel, int> userRepository,
            IRepository<AdministrativeModel, int> administrativeRepository)
        {
            _drivingSchoolRepository = drivingSchoolRepository;
            _userRepository = userRepository;
            _administrativeRepository = administrativeRepository;
        }

        public async Task<AdministrativeModel> Handle(AdministrativeCreateInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (_userRepository.Exists(e => e.Email.Equals(request.Email)))
                    throw new CustomException("Email já em uso");

                var administrative = new AdministrativeModel
                {
                    Name = request.Name.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Cpf = request.Cpf,
                    Identity = request.Identity,
                    Origin = request.Origin.ToUpper(),
                    Birthdate = request.Birthdate,
                    Address = request.Address,
                    DrivingSchoolId = request.DrivingSchoolId,
                    PhonesNumbers = request.PhonesNumbers,
                    User = new()
                    {
                        Email = request.Email.ToUpper(),
                        Name = request.Name.ToUpper(),
                        CreatedAt = DateTime.Now,
                        Roles = new List<RolesModel>{
                            new ()
                            {
                                Role = Constants.Roles.ADMINISTRATIVO
                            }
                        }
                    }
                };

                var result = _administrativeRepository.Add(administrative);
                _administrativeRepository.SaveChanges();

                return result;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(AdministrativeCreateInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
