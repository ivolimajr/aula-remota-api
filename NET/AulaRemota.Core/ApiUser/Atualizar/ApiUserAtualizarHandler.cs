using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ApiUser.Atualizar
{
    public class ApiUserAtualizarHandler : IRequestHandler<ApiUserAtualizarInput, ApiUserAtualizarResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserAtualizarHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<ApiUserAtualizarResponse> Handle(ApiUserAtualizarInput request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _authUserRepository.GetByIdAsync(request.Id);
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

                if (!String.IsNullOrEmpty(request.Nome)) entity.Nome = request.Nome.ToUpper();
                if (!String.IsNullOrEmpty(request.UserName)) entity.UserName = request.UserName.ToUpper();

                _authUserRepository.Update(entity);
                await _authUserRepository.SaveChangesAsync();

                return new ApiUserAtualizarResponse()
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
