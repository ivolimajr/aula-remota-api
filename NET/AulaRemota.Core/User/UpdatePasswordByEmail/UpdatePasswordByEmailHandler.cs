using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;

namespace AulaRemota.Core.User.UpdatePasswordByEmail
{
    public class UpdatePasswordByEmailHandler : IRequestHandler<UpdatePasswordByEmailInput, bool>
    {
        private readonly IRepository<UserModel> _usuarioRepository;

        public UpdatePasswordByEmailHandler(IRepository<UserModel> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(UpdatePasswordByEmailInput request, CancellationToken cancellationToken)
        {
            if (request.Email == null) throw new CustomException("Busca Inválida");

            try
            {
                var user = await _usuarioRepository.FindAsync(e => e.Email == request.Email);
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
