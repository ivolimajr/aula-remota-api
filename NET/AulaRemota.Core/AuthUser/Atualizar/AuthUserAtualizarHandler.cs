using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserAtualizarHandler : IRequestHandler<AuthUserAtualizarInput,AuthUserAtualizarResponse>
    {
        private readonly IRepository<AuthUserModel> _authUserRepository;

        public AuthUserAtualizarHandler(IRepository<AuthUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserAtualizarResponse> Handle(AuthUserAtualizarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            //BUSCA O OBJETO A SER ATUALIZADO
            var entity = _authUserRepository.GetById(request.Id);
            if (entity == null) throw new HttpClientCustomException("Não Encontrado");


            if(request.Nome != null) entity.Nome = request.Nome.ToUpper();
            if(request.UserName != null) entity.UserName = request.UserName.ToUpper();

            try
            {
                AuthUserModel result = await _authUserRepository.UpdateAsync(entity);

                var AuthUser = new AuthUserAtualizarResponse();
                AuthUser.Id         = result.Id; 
                AuthUser.Nome   = result.Nome;
                AuthUser.UserName   = result.UserName;

                return AuthUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
