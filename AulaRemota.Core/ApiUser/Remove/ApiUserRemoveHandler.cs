using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net;

namespace AulaRemota.Core.ApiUser.Remove
{
    public class ApiUserRemoveHandler : IRequestHandler<ApiUserRemoveInput, bool>
    {
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public ApiUserRemoveHandler(IRepository<ApiUserModel, int> authUserRepository) => _authUserRepository = authUserRepository;

        public async Task<bool> Handle(ApiUserRemoveInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.FindAsync(request.Id);
                Check.NotNull(result, "Não encontrado");

                _authUserRepository.Delete(result);
                _authUserRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                object result = new { id = request.Id };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserRemoveHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = result
                });
            }
        }
    }
}
