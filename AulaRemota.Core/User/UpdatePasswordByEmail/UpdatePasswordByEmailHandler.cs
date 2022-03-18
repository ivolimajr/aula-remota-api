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
        private readonly IRepository<UserModel, int>_userRepository;

        public UpdatePasswordByEmailHandler(IRepository<UserModel, int>userRepository) =>
            _userRepository = userRepository;

        public async Task<bool> Handle(UpdatePasswordByEmailInput request, CancellationToken cancellationToken)
        {
            if (request.Email == null) throw new CustomException("Busca Inválida");

            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(e => e.Email == request.Email);
                if (user == null) throw new CustomException("Não Encontrado");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password);
                if (!checkPass) throw new CustomException("Senha atual inválida");

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UpdatePasswordByEmailHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized
                });
            }
        }
    }
}
