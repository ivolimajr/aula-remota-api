using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserCriarHandler : IRequestHandler<ApiUserCriarInput, ApiUserCriarResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;
        private readonly IRepository<RolesModel> _rolesRepository;

        public ApiUserCriarHandler(IRepository<ApiUserModel> authUserRepository, IRepository<RolesModel> rolesRepository)
        {
            _authUserRepository = authUserRepository;
            _rolesRepository = rolesRepository;
        }

        public async Task<ApiUserCriarResponse> Handle(ApiUserCriarInput request, CancellationToken cancellationToken)
        {
            try
            {
                _authUserRepository.CreateTransaction();
                var userValidate = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (userValidate != null) throw new HttpClientCustomException("Usuário já cadastrado", 400);

                var user = new ApiUserModel()
                {
                    Nome = request.Nome.ToUpper(),
                    UserName = request.UserName.ToUpper(),
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = new List<RolesModel>()
                    {
                        new RolesModel()
                        {
                            Role = Constants.Roles.APIUSER
                        }
                    }
                };
                var userResult = await _authUserRepository.CreateAsync(user);

                _authUserRepository.Commit();
                _authUserRepository.Save();

                return new ApiUserCriarResponse
                {
                    Id = userResult.Id,
                    Nome = userResult.Nome,
                    UserName = userResult.UserName,
                    Roles = user.Roles
                };
            }
            catch (Exception e)
            {
                _authUserRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _authUserRepository.Context.Dispose();
            }
        }
    }
}
