using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AulaRemota.Infra.Entity.Auto_Escola;

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
            if (request.Email == string.Empty) throw new HttpClientCustomException("Valores Inválidos");

            try
            {
                var result = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (result == null) throw new HttpClientCustomException("Não Encontrado");

                if(result.status == 0) throw new HttpClientCustomException("Usuário Removido");
                if(result.status == 2) throw new HttpClientCustomException("Usuário Inativo");

                bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
                if (!checkPass) throw new HttpClientCustomException("Credenciais Inválidas");

                if(result.NivelAcesso >= 10 && result.NivelAcesso <20)
                {
                    result.Id = _usuarioRepository.Context.Set<EdrivingModel>().Where(e => e.UsuarioId == result.Id).FirstOrDefault().Id;
                }
                if(result.NivelAcesso >= 20 && result.NivelAcesso <30)
                {
                    result.Id = _usuarioRepository.Context.Set<ParceiroModel>().Where(e => e.UsuarioId == result.Id).FirstOrDefault().Id;
                }
                if(result.NivelAcesso >= 30 && result.NivelAcesso <40)
                {
                    result.Id = _usuarioRepository.Context.Set<AutoEscolaModel>().Where(e => e.UsuarioId == result.Id).FirstOrDefault().Id;
                }

                return new UsuarioLoginResponse
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    NivelAcesso = result.NivelAcesso,
                    status = result.status,
                };

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}