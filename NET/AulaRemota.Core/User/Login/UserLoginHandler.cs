using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AulaRemota.Infra.Entity.DrivingSchool;
using Microsoft.EntityFrameworkCore;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;

namespace AulaRemota.Core.User.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginInput, UserLoginResponse>
    {
        private readonly IRepository<UserModel, int>_usuarioRepository;

        public UserLoginHandler(IRepository<UserModel, int>usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
                var result = await _usuarioRepository.Context.Set<UserModel>().Include(e => e.Roles).Where(e => e.Email.Equals(request.Email)).FirstOrDefaultAsync();

                if (result == null || !result.Email.Equals(request.Email.ToUpper()))
                    throw new CustomException("Credenciais Inválidas", HttpStatusCode.Unauthorized);

                if (result.Status == 0) throw new CustomException("Usuário Removido", HttpStatusCode.Unauthorized);
                if (result.Status == 2) throw new CustomException("Usuário Inativo", HttpStatusCode.Forbidden);

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
                if (!checkPass) throw new CustomException("Credenciais Inválidas", HttpStatusCode.Unauthorized);

                if (result.Roles.Where(x => x.Role == Constants.Roles.EDRIVING).Any())
                    result.Id = _usuarioRepository.Context.Set<EdrivingModel>().Where(e => e.UserId == result.Id).FirstOrDefault().Id;
                if (result.Roles.Where(x => x.Role == Constants.Roles.PARCEIRO).Any())
                    result.Id = _usuarioRepository.Context.Set<PartnnerModel>().Where(e => e.UserId == result.Id).FirstOrDefault().Id;
                if (result.Roles.Where(x => x.Role == Constants.Roles.AUTOESCOLA).Any())
                    result.Id = _usuarioRepository.Context.Set<DrivingSchoolModel>().Where(e => e.UserId == result.Id).FirstOrDefault().Id;

                return new UserLoginResponse
                {
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    Status = result.Status,
                    Roles = result.Roles
                };

            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UserLoginHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}