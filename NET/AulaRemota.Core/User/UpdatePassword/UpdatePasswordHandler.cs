using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;
using System.Net;

namespace AulaRemota.Core.User.UpdatePassword
{
    public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordInput, bool>
    {
        private readonly IRepository<UserModel, int>_usuarioRepository;

        public UpdatePasswordHandler(IRepository<UserModel, int>usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(UpdatePasswordInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var user = await _usuarioRepository.FindAsync(request.Id);
                if (user == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password);
                if (!checkPass) throw new CustomException("Senha atual inválida", HttpStatusCode.Unauthorized);

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                _usuarioRepository.Update(user);
                await _usuarioRepository.SaveChangesAsync();

                return true;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UpdatePasswordInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }

        }
    }
}
