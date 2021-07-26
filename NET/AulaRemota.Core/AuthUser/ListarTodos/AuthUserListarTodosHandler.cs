using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarTodosHandler : IRequestHandler<AuthUserListarTodosInput, AuthUserListarTodosResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public AuthUserListarTodosHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserListarTodosResponse> Handle(AuthUserListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.Context.Set<ApiUserModel>()
                    .OrderBy(u => u.Id).ToListAsync();

                return new AuthUserListarTodosResponse { Items = result };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
