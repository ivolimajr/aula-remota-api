using AulaRemota.Core.Arquivo.UploadAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using AulaRemota.Shared.Helpers.Constants;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AulaRemota.Core.Arquivo.Deletar;

namespace AulaRemota.Core.AutoEscola.Update
{
    public class AutoEscolaUpdateHandler : IRequestHandler<AutoEscolaUpdateInput, AutoEscolaModel>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;
        private readonly IRepository<ArquivoModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public AutoEscolaUpdateHandler(
            IRepository<AutoEscolaModel> autoEscolaRepository,
            IRepository<UsuarioModel> usuarioRepository,
            IRepository<TelefoneModel> telefoneRepository,
            IRepository<ArquivoModel> arquivoRepository,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }
        public async Task<AutoEscolaModel> Handle(AutoEscolaUpdateInput request, CancellationToken cancellationToken)
        {
            //Cria uma lista para receber os arquivos
            var fileList = new List<ArquivoModel>();
            try
            {
                _autoEscolaRepository.CreateTransaction();

                var autoEscolaDb = _autoEscolaRepository.Context.Set<AutoEscolaModel>().Where(e => e.Id.Equals(request.Id))
                                                .Include(e => e.Telefones).Include(e => e.Arquivos).Include(e => e.Usuario).Include(e => e.Endereco).FirstOrDefault();

                if (autoEscolaDb == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                if (!String.IsNullOrWhiteSpace(request.RazaoSocial)) autoEscolaDb.RazaoSocial = request.RazaoSocial;
                if (!String.IsNullOrWhiteSpace(request.NomeFantasia))
                {
                    autoEscolaDb.NomeFantasia = request.NomeFantasia;
                    autoEscolaDb.Usuario.Nome = request.NomeFantasia;
                }
                if (!String.IsNullOrWhiteSpace(request.Descricao)) autoEscolaDb.Descricao = request.Descricao;
                if (!String.IsNullOrWhiteSpace(request.Site)) autoEscolaDb.Site = request.Site;
                if (!String.IsNullOrWhiteSpace(request.Cep)) autoEscolaDb.Endereco.Cep = request.Cep;
                if (!String.IsNullOrWhiteSpace(request.Uf)) autoEscolaDb.Endereco.Uf = request.Uf;
                if (!String.IsNullOrWhiteSpace(request.EnderecoLogradouro)) autoEscolaDb.Endereco.EnderecoLogradouro = request.EnderecoLogradouro;
                if (!String.IsNullOrWhiteSpace(request.Bairro)) autoEscolaDb.Endereco.Bairro = request.Bairro;
                if (!String.IsNullOrWhiteSpace(request.Cidade)) autoEscolaDb.Endereco.Cidade = request.Cidade;
                if (!String.IsNullOrWhiteSpace(request.Numero)) autoEscolaDb.Endereco.Numero = request.Numero;
                if (request.DataFundacao >= DateTime.Today) throw new CustomException("Data da fundação inválida", HttpStatusCode.BadRequest);
                if (request.DataFundacao.Year > 1700) autoEscolaDb.DataFundacao = request.DataFundacao;


                if (!String.IsNullOrWhiteSpace(request.InscricaoEstadual) && !request.InscricaoEstadual.Equals(autoEscolaDb.InscricaoEstadual))
                    if (_autoEscolaRepository.Exists(e => e.InscricaoEstadual == request.InscricaoEstadual))
                        throw new CustomException("Inscrição estadual já em uso.", HttpStatusCode.BadRequest);
                if (!String.IsNullOrWhiteSpace(request.Cnpj) && !request.Cnpj.Equals(autoEscolaDb.Cnpj))
                    if (_autoEscolaRepository.Exists(e => e.Cnpj == request.Cnpj))
                        throw new CustomException("CNPJ já em uso.", HttpStatusCode.BadRequest);
                if (!String.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(autoEscolaDb.Email))
                    if (_autoEscolaRepository.Exists(e => e.Email == request.Email))
                        throw new CustomException("Email estadual já em uso.", HttpStatusCode.BadRequest);

                if (!String.IsNullOrWhiteSpace(request.InscricaoEstadual)) autoEscolaDb.InscricaoEstadual = request.InscricaoEstadual;
                if (!String.IsNullOrWhiteSpace(request.Cnpj)) autoEscolaDb.Cnpj = request.Cnpj;
                if (!String.IsNullOrWhiteSpace(request.Email)) autoEscolaDb.Email = request.Email;

                /*Faz o upload dos arquivos no azure e tem como retorno uma lista com os dados do upload
                 * @return nome, formato e destino
                 */

                if (request.Arquivos != null)
                {
                    var fileResult = await _mediator.Send(new ArquivoUploadAzureInput
                    {
                        Arquivos = request.Arquivos,
                        TipoUsuario = Constants.Roles.AUTOESCOLA
                    });
                    //Salva no banco todas as informações dos arquivos do upload
                    foreach (var item in fileResult.Arquivos)
                    {
                        var arquivo = await _arquivoRepository.CreateAsync(item);
                        fileList.Add(item);
                    }
                    foreach (var item in fileList)
                    {
                        autoEscolaDb.Arquivos.Add(item);
                    }
                }

                //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                if (request.Telefones != null && request.Telefones.Count > 0)
                {
                    foreach (var item in request.Telefones)
                    {
                        var result = autoEscolaDb.Telefones.Where(e => e.Telefone.Equals(item.Telefone)).FirstOrDefault();
                        if (result == null)
                        {
                            var phoneResult = await _telefoneRepository.FindAsync(u => u.Telefone == item.Telefone);
                            if (phoneResult != null) throw new CustomException("Telefone: " + phoneResult.Telefone + " já em uso");
                        }
                    }
                }

                _autoEscolaRepository.Update(autoEscolaDb);
                _autoEscolaRepository.Commit();
                _autoEscolaRepository.Save();

                return autoEscolaDb;
            }
            catch (CustomException e)
            {
                _autoEscolaRepository.Rollback();
                await _mediator.Send(new ArquivoDeletarInput()
                {
                    TipoUsuario = Constants.Roles.AUTOESCOLA,
                    Arquivos = fileList
                });
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(AutoEscolaUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
            finally
            {
                _autoEscolaRepository.Context.Dispose();
            }
        }
    }
}
