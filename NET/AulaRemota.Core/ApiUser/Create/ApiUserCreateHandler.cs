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
using System.Net;

namespace AulaRemota.Core.ApiUser.Create
{
    public class ApiUserCreateHandler : IRequestHandler<ApiUserCreateInput, ApiUserCreateResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserCreateHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserCreateResponse> Handle(ApiUserCreateInput request, CancellationToken cancellationToken)
        {
            try
            {
                _authUserRepository.CreateTransaction();
                var userValidate = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (userValidate != null) throw new CustomException("Usuário já cadastrado", HttpStatusCode.BadRequest);

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
                var userResult = await _authUserRepository.CreateAsync(user);

                _authUserRepository.Commit();
                _authUserRepository.Save();

                return new ApiUserCreateResponse
                {
                    Id = userResult.Id,
                    Name = userResult.Name,
                    UserName = userResult.UserName,
                    Roles = user.Roles
                };
            }
            catch (CustomException e)
            {
                _authUserRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserCreateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
            finally
            {
                _authUserRepository.Context.Dispose();
            }
        }
    }
}
