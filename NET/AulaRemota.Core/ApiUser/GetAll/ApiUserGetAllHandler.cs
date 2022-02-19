using AulaRemota.Core.Services;
using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ApiUser.GetAll

{
    public class ApiUserGetAllHandler : IRequestHandler<ApiUserGetAllInput, List<ApiUserModel>>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;
        private readonly AuthenticatedUserServices _authUserServices;

        public ApiUserGetAllHandler(IRepository<ApiUserModel> authUserRepository, AuthenticatedUserServices authenticatedUserServices)
        {
            _authUserRepository = authUserRepository;
            _authUserServices = authenticatedUserServices;
        }

        public async Task<List<ApiUserModel>> Handle(ApiUserGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = _authUserServices.GetRoles();
                var email = _authUserServices.Email;
                var result = await _authUserRepository.Context.Set<ApiUserModel>()
                    .Include(r => r.Roles).ToListAsync();
                foreach (var item in result)
                {
                    item.RefreshToken = default;
                    item.Password = default;
                }
                return result;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserGetAllHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
