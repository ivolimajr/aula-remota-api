using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;

namespace AulaRemota.Core.Usuario.AtualizarSenhaPorEmail
{
    public class UsuarioAtualizarSenhaPorEmailHandler : IRequestHandler<UsuarioAtualizarSenhaPorEmailInput, bool>
    {
        private readonly IRepository<UsuarioModel> _usuarioRepository;

        public UsuarioAtualizarSenhaPorEmailHandler(IRepository<UsuarioModel> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(UsuarioAtualizarSenhaPorEmailInput request, CancellationToken cancellationToken)
        {
            if (request.Email == null) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var user = await _usuarioRepository.FindAsync(e => e.Email == request.Email);
                if (user == null) throw new HttpClientCustomException("Não Encontrado");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.SenhaAtual, user.Password);
                if (!checkPass) throw new HttpClientCustomException("Senha atual inválida");

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NovaSenha);
                _usuarioRepository.Update(user);
                await _usuarioRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
