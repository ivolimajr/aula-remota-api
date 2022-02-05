using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarPorIdHandler : IRequestHandler<AuthUserListarPorIdInput, ApiUserModel>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public AuthUserListarPorIdHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserModel> Handle(AuthUserListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var result = await _authUserRepository.GetByIdAsync(request.Id); 
                if (result == null) throw new HttpClientCustomException("Não Encontrado");
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
