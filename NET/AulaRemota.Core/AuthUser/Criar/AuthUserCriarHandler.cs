using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserCriarHandler : IRequestHandler<AuthUserCriarInput, AuthUserCriarResponse>
    {
        private readonly IRepository<AuthUserModel> _authUserRepository;

        public AuthUserCriarHandler(IRepository<AuthUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserCriarResponse> Handle(AuthUserCriarInput request, CancellationToken cancellationToken)
        {
            var userExists = _authUserRepository.Find(u => u.UserName == request.UserName);
            if (userExists != null) throw new HttpClientCustomException("Usuário já cadastrado");

            request.FullName = request.FullName.ToUpper();
            request.UserName = request.UserName.ToUpper();
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var AuthUserModel = new AuthUserModel();
            AuthUserModel.FullName = request.FullName;
            AuthUserModel.UserName = request.UserName;
            AuthUserModel.Password = request.Password;

            try
            {
                AuthUserModel authUser = await _authUserRepository.CreateAsync(AuthUserModel);

                var authUserResult = new AuthUserCriarResponse
                {
                    Id = authUser.Id,
                    FullName = authUser.FullName,
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
