using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserDeletarHandler : IRequestHandler<ApiUserDeletarInput, bool>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserDeletarHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<bool> Handle(ApiUserDeletarInput request, CancellationToken cancellationToken)
        {
            try
            {
                ApiUserModel authUser = await _authUserRepository.GetByIdAsync(request.Id);
                if (authUser == null) throw new HttpClientCustomException("Não encontrado");

                _authUserRepository.Delete(authUser);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
