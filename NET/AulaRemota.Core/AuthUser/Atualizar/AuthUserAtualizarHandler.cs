using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Atualizar
{
    public class AuthUserAtualizarHandler : IRequestHandler<AuthUserAtualizarInput, AuthUserAtualizarResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public AuthUserAtualizarHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserAtualizarResponse> Handle(AuthUserAtualizarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválido");

            try
            {
                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = await _authUserRepository.GetByIdAsync(request.Id);
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

                if (request.Nome != null) entity.Nome = request.Nome.ToUpper();
                if (request.UserName != null) entity.UserName = request.UserName.ToUpper();

                _authUserRepository.Update(entity);
                await _authUserRepository.SaveChangesAsync();

                return new AuthUserAtualizarResponse()
                {
                    Id = entity.Id,
                    Nome = entity.Nome,
                    UserName = entity.UserName
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
