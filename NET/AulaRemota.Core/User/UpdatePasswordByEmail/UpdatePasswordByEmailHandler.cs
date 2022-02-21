using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Net;

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
                if (user == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password);
                if (!checkPass) throw new CustomException("Senha atual inválida");

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
                    ModelName = nameof(UpdatePasswordByEmailHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
