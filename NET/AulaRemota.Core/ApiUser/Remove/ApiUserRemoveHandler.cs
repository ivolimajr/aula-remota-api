using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.ApiUser.Remove
{
    public class ApiUserRemoveHandler : IRequestHandler<ApiUserRemoveInput, bool>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserRemoveHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<bool> Handle(ApiUserRemoveInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.GetByIdAsync(request.Id);
                if (result == null) throw new CustomException("Não encontrado");

                _authUserRepository.Delete(result);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
    }
}
