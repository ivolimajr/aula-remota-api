using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System;

namespace AulaRemota.Core.ApiAuth.RevokeToken
{
    class RevokeTokenHandler : IRequestHandler<RevokeTokenInput, string>
    {
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public RevokeTokenHandler(IRepository<ApiUserModel, int> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<string> Handle(RevokeTokenInput request, CancellationToken cancellationToken)
        {
            if (request.UserName == string.Empty) throw new CustomException("Parâmetros Inválidos");

            try
            {
                ApiUserModel authUser = _authUserRepository.FirstOrDefault(u => u.UserName == request.UserName);
                if (authUser == null) throw new CustomException("Credenciais Não encontrada");

                ApiUserModel User = await _authUserRepository.FindAsync(authUser.Id);

                User.RefreshToken = null;
                _authUserRepository.Update(User);
                await _authUserRepository.SaveChangesAsync();
                return "Usuário da Api Removido";

            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RevokeTokenHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized
                });
            }

        }
    }
}
