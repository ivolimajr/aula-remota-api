using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;

namespace AulaRemota.Core.Usuario.AtualizarSenha
{
    public class UsuarioAtualizarSenhaHandler : IRequestHandler<UsuarioAtualizarSenhaInput, bool>
    {
        private readonly IRepository<UsuarioModel> _usuarioRepository;

        public UsuarioAtualizarSenhaHandler(IRepository<UsuarioModel> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(UsuarioAtualizarSenhaInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var user = await _usuarioRepository.GetByIdAsync(request.Id);
                if (user == null) throw new CustomException("Não Encontrado");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.SenhaAtual, user.Password);
                if (!checkPass) throw new CustomException("Senha atual inválida");

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
