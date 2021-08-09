using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Usuario.Login
{
    public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginInput, UsuarioLoginResponse>
    {
        private readonly IRepository<UsuarioModel> _UsuarioRepository;

        public UsuarioLoginHandler(IRepository<UsuarioModel> UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
        }

        public async Task<UsuarioLoginResponse> Handle(UsuarioLoginInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new HttpClientCustomException("Valores Inválidos");

            try
            {
                var result = await _UsuarioRepository.FindAsync(u => u.Email == request.Email);
                if (result == null) throw new HttpClientCustomException("Não Encontrado");

                if(result.status == 0) throw new HttpClientCustomException("Usuário Removido");
                if(result.status == 2) throw new HttpClientCustomException("Usuário Inativo");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
                if (!checkPass) throw new HttpClientCustomException("Credenciais Inválidas");

                return new UsuarioLoginResponse
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