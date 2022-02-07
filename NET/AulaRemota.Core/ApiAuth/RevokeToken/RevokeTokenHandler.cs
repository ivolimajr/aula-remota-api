using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AulaRemota.Core.ApiAuth.RevokeToken
{
    class RevokeTokenHandler : IRequestHandler<RevokeTokenInput, string>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public RevokeTokenHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<string> Handle(RevokeTokenInput request, CancellationToken cancellationToken)
        {
            if (request.UserName == string.Empty) throw new CustomException("Parâmetros Inválidos", HttpStatusCode.BadRequest);

            try
            {
                ApiUserModel authUser = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (authUser == null) throw new CustomException("Credenciais Não encontrada", HttpStatusCode.Unauthorized);

                ApiUserModel usuario = await _authUserRepository.GetByIdAsync(authUser.Id);

                usuario.RefreshToken = null;
                _authUserRepository.Update(usuario);
                await _authUserRepository.SaveChangesAsync();
                return "Usuário da Api Removido";

            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RevokeTokenHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }

        }
    }
}
