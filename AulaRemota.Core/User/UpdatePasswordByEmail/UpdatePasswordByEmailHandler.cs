using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Net;
using System;

namespace AulaRemota.Core.User.UpdatePasswordByEmail
{
    public class UpdatePasswordByEmailHandler : IRequestHandler<UpdatePasswordByEmailInput, bool>
    {
        private readonly IRepository<UserModel, int> _userRepository;

        public UpdatePasswordByEmailHandler(IRepository<UserModel, int> userRepository) =>
            _userRepository = userRepository;

        public async Task<bool> Handle(UpdatePasswordByEmailInput request, CancellationToken cancellationToken)
        {
            Check.NotNull(request.Email, "Busca Inválida");

            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(e => e.Email == request.Email);
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
                object result = new { userEmail = request.Email };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UpdatePasswordByEmailHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Data = result
                });
            }
        }
    }
}
