using AulaRemota.Core.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Usuario.Criar
{
    class UsuarioCriarHandler : IRequestHandler<UsuarioCriarInput, UsuarioCriarResponse>
    {
        private readonly IRepository<UsuarioModel> _usuarioRepository;

        public UsuarioCriarHandler(IRepository<UsuarioModel> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioCriarResponse> Handle(UsuarioCriarInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new HttpClientCustomException("Valores Inválidos");

            var userExists = _usuarioRepository.Find(u => u.Email == request.Email);
            if (userExists != null) throw new HttpClientCustomException("Email já em uso");

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var usuario = new UsuarioModel
            {
                FullName = request.FullName.ToUpper(),
                Email = request.Email.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            try
            {
                UsuarioModel user = await _usuarioRepository.CreateAsync(usuario);
                return new UsuarioCriarResponse
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email
                };

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
