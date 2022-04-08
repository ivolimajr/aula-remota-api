using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;
using System;

namespace AulaRemota.Core.User.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginInput, UserLoginResponse>
    {
        private readonly IRepository<UserModel, int> _userRepository;
        private readonly IRepository<PartnnerModel, int> _partnnerRepository;
        private readonly IRepository<EdrivingModel, int> _edrivingRepository;
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;
        private readonly IRepository<DrivingSchoolModel, int> _drivingSchoolRepository;

        public UserLoginHandler(
            IRepository<UserModel, int> userRepository,
            IRepository<PartnnerModel, int> partnnerRepository,
            IRepository<EdrivingModel, int> edrivingRepository,
            IRepository<AdministrativeModel, int> administrativeRepository,
            IRepository<DrivingSchoolModel, int> drivingSchoolRepository
            )
        {
            _userRepository = userRepository;
            _partnnerRepository = partnnerRepository;
            _edrivingRepository = edrivingRepository;
            _administrativeRepository = administrativeRepository;
            _drivingSchoolRepository = drivingSchoolRepository;
        }

        public async Task<UserLoginResponse> Handle(UserLoginInput request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, "Valores Inválidos");

            try
            {
                var userEntity = await _userRepository
                    .Where(e => e.Email.Equals(request.Email))
                    .Include(e => e.Roles)
                    .FirstOrDefaultAsync();

                Check.NotNull(userEntity, "Credenciais Inválidas");

                if (userEntity.Status == Constants.Status.INATIVO) throw new CustomException("Usuário Inativo");
                if (userEntity.Status == Constants.Status.REMOVIDO) throw new CustomException("Usuário Removido");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, userEntity.Password);
                Check.IsTrue(checkPass, "Credenciais Inválidas");

                UserLoginResponse userResult = new();

                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.EDRIVING).Any())
                {
                    var result = _edrivingRepository.Where(e => e.UserId == userEntity.Id).FirstOrDefault();
                    userResult.Id = result.Id;
                    userResult.UserId = userEntity.Id;
                    userResult.Email = result.Email;
                    userResult.Name = result.Name;
                    userResult.Status = userEntity.Status;
                    userResult.Roles = userEntity.Roles;
                }

                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.PARCEIRO).Any())
                {
                    var result = _partnnerRepository.Where(e => e.UserId == userEntity.Id).Include(e => e.Address).FirstOrDefault();
                    userResult.Id = result.Id;
                    userResult.UserId = userEntity.Id;
                    userResult.Email = result.Email;
                    userResult.Name = result.Name;
                    userResult.Status = userEntity.Status;
                    userResult.Roles = userEntity.Roles;
                    userResult.Address = result.Address;
                }

                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.AUTOESCOLA).Any())
                {
                    var result = _drivingSchoolRepository.Where(e => e.UserId == userEntity.Id).Include(e => e.Address).FirstOrDefault();
                    userResult.Id = result.Id;
                    userResult.UserId = userEntity.Id;
                    userResult.Email = result.Email;
                    userResult.Name = result.FantasyName;
                    userResult.Status = userEntity.Status;
                    userResult.Roles = userEntity.Roles;
                    userResult.Address = result.Address;
                }

                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.ADMINISTRATIVO).Any())
                {
                    var result = _administrativeRepository.Where(e => e.UserId == userEntity.Id).Include(e => e.Address).FirstOrDefault();
                    userResult.Id = result.Id;
                    userResult.UserId = userEntity.Id;
                    userResult.Email = result.Email;
                    userResult.Name = result.Name;
                    userResult.Status = userEntity.Status;
                    userResult.Roles = userEntity.Roles;
                    userResult.Address = result.Address;
                }
                return userResult;
            }
            catch (Exception e)
            {
                object result = new
                {
                    userName = request.Email
                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UserLoginHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Data = result
                });
            }
        }
    }
}