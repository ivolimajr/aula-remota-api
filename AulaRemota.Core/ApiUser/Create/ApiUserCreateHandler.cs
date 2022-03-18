using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;
using System;

namespace AulaRemota.Core.ApiUser.Create
{
    public class ApiUserCreateHandler : IRequestHandler<ApiUserCreateInput, ApiUserCreateResponse>
    {
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public ApiUserCreateHandler(IRepository<ApiUserModel, int> authUserRepository) => _authUserRepository = authUserRepository;
        public async Task<ApiUserCreateResponse> Handle(ApiUserCreateInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (_authUserRepository.Exists(u => u.UserName == request.UserName))
                    throw new CustomException("Usuário já cadastrado");

                var user = new ApiUserModel()
                {
                    Name = request.Name.ToUpper(),
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
                var userResult = await _authUserRepository.AddAsync(user);

                _authUserRepository.Save();

                return new ApiUserCreateResponse
                {
                    Id = userResult.Id,
                    Name = userResult.Name,
                    UserName = userResult.UserName,
                    Roles = user.Roles
                };
            }
            catch (Exception e)
            {
                object result = new
                {
                    name = request.Name,
                    userName = request.UserName
                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserCreateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    Data = result
                });
            }
        }
    }
}
