using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserListarPorIdHandler : IRequestHandler<ApiUserListarPorIdInput, ApiUserModel>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserListarPorIdHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserModel> Handle(ApiUserListarPorIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.Context.Set<ApiUserModel>().Include(e => e.Roles).Where(e => e.Id == request.Id).FirstOrDefaultAsync(); 
                if (result == null) throw new HttpClientCustomException("Não Encontrado");
                result.Password = default;
                result.RefreshToken = default;
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
