using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AulaRemota.Core.ApiUser.Update
{
    public class ApiUserUpdateHandler : IRequestHandler<ApiUserUpdateInput, ApiUserUpdateResponse>
    {
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public ApiUserUpdateHandler(IRepository<ApiUserModel, int> authUserRepository) => _authUserRepository = authUserRepository;

        public async Task<ApiUserUpdateResponse> Handle(ApiUserUpdateInput request, CancellationToken cancellationToken)
        {
            try
            {
                var apiUserEntity = await _authUserRepository.FindAsync(request.Id);
                Check.NotNull(apiUserEntity, "Não Encontrado");

                if (string.IsNullOrEmpty(request.UserName) && request.UserName != apiUserEntity.UserName)
                    Check.NotExist(_authUserRepository.Exists(e => e.UserName.Equals(request.UserName)), "Email já em uso");

                apiUserEntity.Name = request.Name ?? apiUserEntity.Name;
                apiUserEntity.UserName = request.UserName ?? request.UserName;

                _authUserRepository.Update(apiUserEntity);
                await _authUserRepository.SaveChangesAsync();

                return new ApiUserUpdateResponse()
                {
                    Id = apiUserEntity.Id,
                    Name = apiUserEntity.Name,
                    UserName = apiUserEntity.UserName
                };
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }

}
