using AulaRemota.Core.Arquivo.Deletar;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.Deletar
{
    public class AutoEscolaDeletarHandler : IRequestHandler<AutoEscolaDeletarInput, bool>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;
        private readonly IRepository<EnderecoModel> _enderecoRepository;
        private readonly IRepository<ArquivoModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public AutoEscolaDeletarHandler(
            IRepository<AutoEscolaModel> autoEscolaRepository,
            IRepository<UsuarioModel> usuarioRepository,
            IRepository<TelefoneModel> telefoneRepository,
            IRepository<EnderecoModel> enderecoRepository,
            IRepository<ArquivoModel> arquivoRepository,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
            _enderecoRepository = enderecoRepository;
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AutoEscolaDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");
            try
            {
                _autoEscolaRepository.CreateTransaction();
                var autoEscola = await _autoEscolaRepository.Context
                    .Set<AutoEscolaModel>()
                    .Include(e => e.Usuario)
                    .Include(e => e.Telefones)
                    .Include(e => e.Endereco)
                    .Include(e => e.Arquivos)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (autoEscola == null) throw new HttpClientCustomException("Não encontrado");

                _autoEscolaRepository.Delete(autoEscola);
                _usuarioRepository.Delete(autoEscola.Usuario);
                _enderecoRepository.Delete(autoEscola.Endereco);

                if (autoEscola.Arquivos.Count > 0)
                {
                    foreach (var item in autoEscola.Arquivos)
                    {
                        if (item != null)
                        {
                            var result = await _mediator.Send(new ArquivoDeletarInput
                            {
                                NomeArquivo = item.Nome,
                                NivelAcesso = 20
                            });
                            if (!result) throw new HttpClientCustomException("Problema ao remover arquivos de contrato.");
                        }

                    }
                }

                foreach (var item in autoEscola.Telefones)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }
                foreach (var item in autoEscola.Arquivos)
                {
                    item.AutoEscola = null;
                    _arquivoRepository.Delete(item);
                }

                _autoEscolaRepository.Save();
                _autoEscolaRepository.Commit();
                return true;

            }
            catch (Exception e)
            {
                _autoEscolaRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _autoEscolaRepository.Context.Dispose();
            }
        }
    }
}
