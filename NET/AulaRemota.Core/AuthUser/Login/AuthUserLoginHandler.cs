using AulaRemota.Core.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AuthUser.Login
{
    public class AuthUserLoginHandler : IRequestHandler<AuthUserLoginInput, AuthUserLoginResponse>
    {
        private readonly IRepository<UsuarioModel> _authUserRepository;

        public AuthUserLoginHandler(IRepository<UsuarioModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<AuthUserLoginResponse> Handle(AuthUserLoginInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new HttpClientCustomException("Valores Inválidos");

            try
            {
                var result = await _authUserRepository.FindAsync(u => u.Email == request.Email);
                if (result == null) throw new HttpClientCustomException("Não Encontrado");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
                if (!checkPass) throw new HttpClientCustomException("Credenciais Inválidas");

                return new AuthUserLoginResponse
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    NivelAcesso = result.NivelAcesso,
                    status = result.status,

                };

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}