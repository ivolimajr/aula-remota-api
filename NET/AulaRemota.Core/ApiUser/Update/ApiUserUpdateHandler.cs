using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ApiUser.Update
{
    public class ApiUserUpdateHandler : IRequestHandler<ApiUserUpdateInput, ApiUserUpdateResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserUpdateHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserUpdateResponse> Handle(ApiUserUpdateInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.GetByIdAsync(request.Id);
                if (result == null) throw new CustomException("Não Encontrado");

                if (!String.IsNullOrEmpty(request.Name)) result.Nome = request.Name.ToUpper();
                if (!String.IsNullOrEmpty(request.UserName)) result.UserName = request.UserName.ToUpper();

                _authUserRepository.Update(result);
                await _authUserRepository.SaveChangesAsync();

                return new ApiUserUpdateResponse()
                {
                    Id = result.Id,
                    Name = result.Nome,
                    UserName = result.UserName
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
