using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Net;
using System;

namespace AulaRemota.Core.User.UpdatePassword
{
    public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordInput, bool>
    {
        private readonly IRepository<UserModel, int> _userRepository;

        public UpdatePasswordHandler(IRepository<UserModel, int> userRepository) => _userRepository = userRepository;

        public async Task<bool> Handle(UpdatePasswordInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var user = await _userRepository.FindAsync(request.Id);
                Check.NotNull(user, "Não Encontrado");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password);
                Check.IsTrue(checkPass, "Senha atual inválida");

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                object result = new
                {
                    userId = request.Id
                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UpdatePasswordInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Data = result
                });
            }

        }
    }
}
