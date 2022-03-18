using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System;

namespace AulaRemota.Core.ApiUser.GetOne
{
    public class ApiUserGetOneHandler : IRequestHandler<ApiUserGetOneInput, ApiUserModel>
    {
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public ApiUserGetOneHandler(IRepository<ApiUserModel, int> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserModel> Handle(ApiUserGetOneInput request, CancellationToken cancellationToken)
        {
            try
            {
                var apiUserEntity = _authUserRepository.Context
                    .Set<ApiUserModel>()
                    .Select(e => new ApiUserModel
                    {
                        Id = e.Id,
                        UserName = e.UserName,
                        Name = e.Name,
                        Roles = e.Roles,
                        RefreshTokenExpiryTime = e.RefreshTokenExpiryTime,
                    })
                    .Where(e => e.Id.Equals(request.Id))
                    .FirstOrDefault();
                if (apiUserEntity == null) throw new CustomException("Não Encontrado");
                apiUserEntity.Password = default;
                apiUserEntity.RefreshToken = default;
                return apiUserEntity;
            }
            catch (Exception e)
            {
                object result = new { id = request.Id };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserGetOneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = result
                });
            }
        }
    }
}
