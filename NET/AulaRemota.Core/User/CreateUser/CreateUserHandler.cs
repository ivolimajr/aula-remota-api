using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.User.CreateUser
{
    class CreateUserHandler : IRequestHandler<CreateUserInput, CreateUserResponse>
    {
        private readonly IRepository<UserModel, int>_usuarioRepository;

        public CreateUserHandler(IRepository<UserModel, int>usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<CreateUserResponse> Handle(CreateUserInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new CustomException("Valores Inválidos");

            var userExists = _usuarioRepository.FirstOrDefault(u => u.Email == request.Email);
            if (userExists != null) throw new CustomException("Email já em uso");

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var User = new UserModel
            {
                Name = request.Name.ToUpper(),
                Email = request.Email.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            try
            {
                UserModel user = await _usuarioRepository.AddAsync(User);
                return new CreateUserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };

            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(CreateUserInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
