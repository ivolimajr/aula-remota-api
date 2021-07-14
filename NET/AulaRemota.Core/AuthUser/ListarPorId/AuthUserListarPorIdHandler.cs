using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarPorIdHandler : IRequestHandler<AuthUserListarPorIdInput, AuthUserListarPorIdResponse>
    {
        private readonly IRepository<AuthUserModel> _authUserRepository;

        public AuthUserListarPorIdHandler(IRepository<AuthUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserListarPorIdResponse> Handle(AuthUserListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                var result = await _authUserRepository.GetByIdAsync(request.Id); 
                if (result == null) throw new HttpClientCustomException("Não Encontrado");
                return new AuthUserListarPorIdResponse { Item = result };
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
