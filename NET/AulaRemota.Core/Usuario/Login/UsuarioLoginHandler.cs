using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;

namespace AulaRemota.Core.Usuario.Login
{
    public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginInput, UsuarioLoginResponse>
    {
        private readonly IRepository<UsuarioModel> _usuarioRepository;

        public UsuarioLoginHandler(IRepository<UsuarioModel> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /**
         * Método responsável por efetuar o login do usuário na plataforma.
         * Diante do nível de acesso do usuário é retornado o ID do tipo de usuário logado.
         */
        public async Task<UsuarioLoginResponse> Handle(UsuarioLoginInput request, CancellationToken cancellationToken)
        {
            if (request.Email == string.Empty) throw new CustomException("Valores Inválidos", HttpStatusCode.BadRequest);

            try
            {
                var result = await _usuarioRepository.Context.Set<UsuarioModel>().Include(e => e.Roles).FirstOrDefaultAsync();

                if (result == null || !result.Email.Equals(request.Email))
                    throw new CustomException("Credenciais Inválidas", HttpStatusCode.Unauthorized);

                if (result.status == 0) throw new CustomException("Usuário Removido", HttpStatusCode.Unauthorized);
                if (result.status == 2) throw new CustomException("Usuário Inativo", HttpStatusCode.Unauthorized);

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
                if (!checkPass) throw new CustomException("Credenciais Inválidas", HttpStatusCode.Unauthorized);

                if (result.Roles.Where(x => x.Role == Constants.Roles.EDRIVING).Any())
                    result.Id = _usuarioRepository.Context.Set<EdrivingModel>().Where(e => e.UsuarioId == result.Id).FirstOrDefault().Id;
                if (result.Roles.Where(x => x.Role == Constants.Roles.PARCEIRO).Any())
                    result.Id = _usuarioRepository.Context.Set<ParceiroModel>().Where(e => e.UsuarioId == result.Id).FirstOrDefault().Id;
                if (result.Roles.Where(x => x.Role == Constants.Roles.AUTOESCOLA).Any())
                    result.Id = _usuarioRepository.Context.Set<AutoEscolaModel>().Where(e => e.UsuarioId == result.Id).FirstOrDefault().Id;

                return new UsuarioLoginResponse
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    status = result.status,
                };

            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UsuarioLoginHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}