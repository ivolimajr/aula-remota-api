using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace AulaRemota.Core.ApiUser.GetOne
{
    public class ApiUserGetOneHandler : IRequestHandler<ApiUserGetOneInput, ApiUserModel>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserGetOneHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserModel> Handle(ApiUserGetOneInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.Context.Set<ApiUserModel>().Include(e => e.Roles).Where(e => e.Id == request.Id).FirstOrDefaultAsync(); 
                if (result == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);
                result.Password = default;
                result.RefreshToken = default;
                return result;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserGetOneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
