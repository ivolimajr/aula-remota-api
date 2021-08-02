using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserDeletarHandler : IRequestHandler<AuthUserDeletarInput, bool>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public AuthUserDeletarHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<bool> Handle(AuthUserDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                ApiUserModel authUser = await _authUserRepository.GetByIdAsync(request.Id);
                if (authUser == null) throw new HttpClientCustomException("Não encontrado");

                _authUserRepository.Delete(authUser);
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
