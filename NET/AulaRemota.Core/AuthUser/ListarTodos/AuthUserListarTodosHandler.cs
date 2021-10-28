using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarTodosHandler : IRequestHandler<AuthUserListarTodosInput, List<ApiUserModel>>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public AuthUserListarTodosHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<List<ApiUserModel>> Handle(AuthUserListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                return _authUserRepository.GetAll().ToList();
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
