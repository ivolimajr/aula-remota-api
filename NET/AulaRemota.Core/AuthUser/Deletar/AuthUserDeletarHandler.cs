using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserDeletarHandler : IRequestHandler<AuthUserDeletarInput, string>
    {
        private readonly IRepository<AuthUserModel> _authUserRepository;

        public AuthUserDeletarHandler(IRepository<AuthUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<string> Handle(AuthUserDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                AuthUserModel authUser = await _authUserRepository.GetByIdAsync(request.Id);
                if (authUser == null) throw new HttpClientCustomException("Não encontrado");

                _authUserRepository.Delete(authUser);
                return "Removido Com Sucesso";
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
