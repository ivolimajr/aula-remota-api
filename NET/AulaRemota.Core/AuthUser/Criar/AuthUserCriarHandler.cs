using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
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
            try
            {
                var userExists = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (userExists != null) throw new HttpClientCustomException("Usuário já cadastrado");

                var AuthUserModel = new ApiUserModel()
                {
                    Nome = request.Nome.ToUpper(),
                    UserName = request.UserName.ToUpper(),
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                _authUserRepository.CreateTransaction();
                var authUser = await _authUserRepository.CreateAsync(AuthUserModel);

                _authUserRepository.Commit();
                _authUserRepository.Save();

                return new AuthUserCriarResponse
                {
                    Id = authUser.Id,
                    Nome = authUser.Nome,
                    UserName = authUser.UserName
                };
            }
            catch (Exception e)
            {
                _authUserRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _authUserRepository.Context.Dispose();
            }
        }
    }
}
