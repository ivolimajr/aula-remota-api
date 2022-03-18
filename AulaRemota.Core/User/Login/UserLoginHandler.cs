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

        /**
         * Método responsável por efetuar o login do usuário na plataforma.
         * Diante do nível de acesso do usuário é retornado o ID do tipo de usuário logado.
         */
        public async Task<UserLoginResponse> Handle(UserLoginInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new CustomException("Valores Inválidos");

            try
            {
                var userEntity = await _userRepository
                    .Where(e => e.Email.Equals(request.Email))
                    .Include(e => e.Roles)
                    .FirstOrDefaultAsync();

                if (userEntity == null || !userEntity.Email.Equals(request.Email.ToUpper()))
                    throw new CustomException("Credenciais Inválidas");

                if (userEntity.Status == 0) throw new CustomException("Usuário Removido");
                if (userEntity.Status == 2) throw new CustomException("Usuário Inativo");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, userEntity.Password);
                if (!checkPass) throw new CustomException("Credenciais Inválidas");

                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.EDRIVING).Any())
                    userEntity.Id = _edrivingRepository.Where(e => e.UserId == userEntity.Id).FirstOrDefault().Id;
                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.PARCEIRO).Any())
                    userEntity.Id = _partnnerRepository.Where(e => e.UserId == userEntity.Id).FirstOrDefault().Id;
                if (userEntity.Roles.Where(x => x.Role == Constants.Roles.AUTOESCOLA).Any())
                    userEntity.Id = _drivingSchoolRepository.Where(e => e.UserId == userEntity.Id).FirstOrDefault().Id;

                return new UserLoginResponse
                {
                    Id = userEntity.Id,
                    Name = userEntity.Name,
                    Email = userEntity.Email,
                    Status = userEntity.Status,
                    Roles = userEntity.Roles
                };

            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UserLoginHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized
                });
            }
        }
    }
}