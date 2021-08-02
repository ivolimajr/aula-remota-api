using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserCriarHandler : IRequestHandler<AuthUserCriarInput, AuthUserCriarResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public AuthUserCriarHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserCriarResponse> Handle(AuthUserCriarInput request, CancellationToken cancellationToken)
        {
            var userExists = _authUserRepository.Find(u => u.UserName == request.UserName);
            if (userExists != null) throw new HttpClientCustomException("Usuário já cadastrado");

            request.Nome = request.Nome.ToUpper();
            request.UserName = request.UserName.ToUpper();
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var AuthUserModel = new ApiUserModel();
            AuthUserModel.Nome = request.Nome;
            AuthUserModel.UserName = request.UserName;
            AuthUserModel.Password = request.Password;

            try
            {
                ApiUserModel authUser = await _authUserRepository.CreateAsync(AuthUserModel);

                var authUserResult = new AuthUserCriarResponse
                {
                    Id = authUser.Id,
                    Nome = authUser.Nome,
                    UserName = authUser.UserName
                };
                return authUserResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
