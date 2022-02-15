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

namespace AulaRemota.Core.AutoEscola.Atualizar
{
    public class AutoEscolaAtualizarHandler : IRequestHandler<AutoEscolaAtualizarInput, AutoEscolaAtualizarResponse>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;
        private readonly IRepository<ArquivoModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public AutoEscolaAtualizarHandler(
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
        public async Task<AutoEscolaAtualizarResponse> Handle(AutoEscolaAtualizarInput request, CancellationToken cancellationToken)
        {
            try
            {
                _autoEscolaRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var userResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                var autoEscolaResult = _autoEscolaRepository.GetById(request.Id);
                if (userResult == null || autoEscolaResult == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                if (!String.IsNullOrWhiteSpace(request.RazaoSocial)) autoEscolaResult.RazaoSocial = request.RazaoSocial;
                if (!String.IsNullOrWhiteSpace(request.NomeFantasia)) autoEscolaResult.NomeFantasia = request.NomeFantasia;
                if (!String.IsNullOrWhiteSpace(request.InscricaoEstadual)) autoEscolaResult.InscricaoEstadual = request.InscricaoEstadual;
                if (!String.IsNullOrWhiteSpace(request.Descricao)) autoEscolaResult.Descricao = request.Descricao;
                if (!String.IsNullOrWhiteSpace(request.Site)) autoEscolaResult.Site = request.Site;
                if (!String.IsNullOrWhiteSpace(request.Cnpj)) autoEscolaResult.Cnpj = request.Cnpj;
                if (!String.IsNullOrWhiteSpace(request.Uf)) autoEscolaResult.Endereco.Uf = request.Uf;
                if (!String.IsNullOrWhiteSpace(request.Cep)) autoEscolaResult.Endereco.Cep = request.Cep;
                if (!String.IsNullOrWhiteSpace(request.Uf)) autoEscolaResult.Endereco.Uf = request.Uf;
                if (!String.IsNullOrWhiteSpace(request.EnderecoLogradouro)) autoEscolaResult.Endereco.EnderecoLogradouro = request.EnderecoLogradouro;
                if (!String.IsNullOrWhiteSpace(request.Bairro)) autoEscolaResult.Endereco.Bairro = request.Bairro;
                if (!String.IsNullOrWhiteSpace(request.Cidade)) autoEscolaResult.Endereco.Cidade = request.Cidade;
                if (!String.IsNullOrWhiteSpace(request.Numero)) autoEscolaResult.Endereco.Numero = request.Numero;
                autoEscolaResult.DataFundacao = request.DataFundacao;

                //Cria uma lista para receber os arquivos
                var listaArquivos = new List<ArquivoModel>();

                /*Faz o upload dos arquivos no azure e tem como retorno uma lista com os dados do upload
                 * @return nome, formato e destino
                 */
                var arquivoResult = await _mediator.Send(new ArquivoUploadAzureInput
                {
                    Arquivos = request.Arquivos,
                     TipoUsuario = Constants.Roles.AUTOESCOLA
                });

                //Salva no banco todas as informações dos arquivos do upload
                foreach (var item in arquivoResult.Arquivos)
                {
                    var arquivo = await _arquivoRepository.CreateAsync(item);
                    listaArquivos.Add(item);
                }

                await _arquivoRepository.SaveChangesAsync();

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cnpjResult = await _autoEscolaRepository.FindAsync(u => u.Cnpj == request.Cnpj);
                if (cnpjResult != null) throw new CustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.Telefones)
                {
                    var telefoneResult = await _telefoneRepository.FindAsync(u => u.Telefone == item.Telefone);
                    if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                }

                //CRIA UM USUÁRIO
                var user = new UsuarioModel()
                {
                    Nome = request.RazaoSocial.ToUpper(),
                    Email = request.Email.ToUpper(),
                    status = 1
                };

                //CRIA UM ENDEREÇO
                var endereco = new EnderecoModel()
                {
                    Bairro = request.Bairro.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    Cidade = request.Cidade.ToUpper(),
                    EnderecoLogradouro = request.EnderecoLogradouro.ToUpper(),
                    Numero = request.Numero.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                //CRIA UM EDRIVING
                var autoEscola = new AutoEscolaModel()
                {
                    RazaoSocial = request.RazaoSocial.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Telefones = request.Telefones,
                    DataFundacao = request.DataFundacao,
                    Descricao = request.Descricao,
                    InscricaoEstadual = request.InscricaoEstadual,
                    NomeFantasia = request.NomeFantasia,
                    Site = request.Site,
                    Usuario = user,
                    Endereco = endereco,
                    Arquivos = listaArquivos
                };

                var autoEscolaModel = await _autoEscolaRepository.CreateAsync(autoEscola);

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _autoEscolaRepository.Commit();
                _autoEscolaRepository.Save();

                return new AutoEscolaAtualizarResponse()
                {
                    Id = autoEscolaModel.Id,
                    RazaoSocial = autoEscolaModel.RazaoSocial.ToUpper(),
                    Cnpj = autoEscolaModel.Cnpj.ToUpper(),
                    Email = autoEscolaModel.Email.ToUpper(),
                    Descricao = autoEscolaModel.Descricao,
                    Telefones = autoEscolaModel.Telefones,
                    DataFundacao = autoEscolaModel.DataFundacao,
                    InscricaoEstadual = autoEscolaModel.InscricaoEstadual,
                    NomeFantasia = autoEscolaModel.NomeFantasia,
                    Site = autoEscolaModel.Site,
                    Usuario = autoEscolaModel.Usuario,
                    Endereco = autoEscolaModel.Endereco,
                    Arquivos = autoEscolaModel.Arquivos
                };
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
