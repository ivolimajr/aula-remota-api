using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
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
                Nome = request.Nome.ToUpper(),
                Email = request.Email.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            try
            {
                UsuarioModel user = await _usuarioRepository.CreateAsync(usuario);
                return new UsuarioCriarResponse
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
