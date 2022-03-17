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

        public ApiUserUpdateHandler(IRepository<ApiUserModel, int> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserUpdateResponse> Handle(ApiUserUpdateInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.FindAsync(request.Id);
                if (result == null) throw new CustomException("Não Encontrado");

                if (!String.IsNullOrEmpty(request.Name)) result.Name = request.Name.ToUpper();
                if (!String.IsNullOrEmpty(request.UserName)) result.UserName = request.UserName.ToUpper();

                _authUserRepository.Update(result);
                await _authUserRepository.SaveChangesAsync();

                return new ApiUserUpdateResponse()
                {
                    Id = result.Id,
                    Name = result.Name,
                    UserName = result.UserName
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
