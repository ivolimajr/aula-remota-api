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
        private readonly IRepository<UserModel> _usuarioRepository;

        public CreateUserHandler(IRepository<UserModel> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<CreateUserResponse> Handle(CreateUserInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new CustomException("Valores Inválidos");

            var userExists = _usuarioRepository.Find(u => u.Email == request.Email);
            if (userExists != null) throw new CustomException("Email já em uso");

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var usuario = new UserModel
            {
                Nome = request.Nome.ToUpper(),
                Email = request.Email.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            try
            {
                UserModel user = await _usuarioRepository.CreateAsync(usuario);
                return new CreateUserResponse
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email
                };

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
