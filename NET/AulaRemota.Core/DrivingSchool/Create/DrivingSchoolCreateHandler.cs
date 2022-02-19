using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Core.File.RemoveFromAzure;

namespace AulaRemota.Core.DrivingSchool.Create
{
    public class DrivingSchoolCreateHandler : IRequestHandler<DrivingSchoolCreateInput, DrivingSchoolCreateResponse>
    {
        private readonly IRepository<DrivingSchoolModel> _autoEscolaRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;
        private readonly IRepository<FileModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public DrivingSchoolCreateHandler(
            IRepository<DrivingSchoolModel> autoEscolaRepository,
            IRepository<UserModel> usuarioRepository,
            IRepository<PhoneModel> telefoneRepository,
            IRepository<FileModel> arquivoRepository,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }
        public async Task<DrivingSchoolCreateResponse> Handle(DrivingSchoolCreateInput request, CancellationToken cancellationToken)
        {
            //Cria uma lista para receber os arquivos
            var fileList = new List<FileModel>();
            try
            {
                _autoEscolaRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                if (_usuarioRepository.Exists(u => u.Email == request.Email)) throw new CustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                if (_autoEscolaRepository.Exists(u => u.Cnpj == request.Cnpj)) throw new CustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.Telefones)
                    if (_telefoneRepository.Exists(u => u.Telefone == item.Telefone)) throw new CustomException("Telefone: " + item.Telefone + " já em uso");

                //CRIA UM USUÁRIO
                var user = new UserModel()
                {
                    Nome = request.NomeFantasia.ToUpper(),
                    Email = request.Email.ToUpper(),
                    status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                    Roles = new List<RolesModel>()
                    {
                        new RolesModel()
                        {
                            Role = Constants.Roles.AUTOESCOLA
                        }
                    }
                };

                //CRIA UM ENDEREÇO
                var address = new AddressModel()
                {
                    Bairro = request.Bairro.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    Cidade = request.Cidade.ToUpper(),
                    EnderecoLogradouro = request.EnderecoLogradouro.ToUpper(),
                    Numero = request.Numero.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                /*Faz o upload dos arquivos no azure e tem como retorno uma lista com os dados do upload
                 * @return nome, formato e destino
                 */
                var fileResult = await _mediator.Send(new FileUploadToAzureInput
                {
                    Arquivos = request.Arquivos,
                    TipoUsuario = Constants.Roles.AUTOESCOLA
                });

                //Salva no banco todas as informações dos arquivos do upload
                foreach (var item in fileResult.Arquivos)
                {
                    var arquivo = await _arquivoRepository.CreateAsync(item);
                    fileList.Add(arquivo);
                }

                await _arquivoRepository.SaveChangesAsync();

                //CRIA UM EDRIVING
                var autoEscola = new DrivingSchoolModel()
                {
                    RazaoSocial = request.RazaoSocial.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Telefones = request.Telefones,
                    DataFundacao = request.DataFundacao,
                    Descricao = request.Descricao,
                    InscricaoEstadual = request.InscricaoEstadual,
                    NomeFantasia = request.NomeFantasia.ToUpper(),
                    Site = request.Site.ToUpper(),
                    Usuario = user,
                    Endereco = address,
                    Arquivos = fileList,
                };

                var autoEscolaModel = await _autoEscolaRepository.CreateAsync(autoEscola);

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _autoEscolaRepository.Commit();
                _autoEscolaRepository.Save();

                return new DrivingSchoolCreateResponse()
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
            catch (CustomException e)
            {
                _autoEscolaRepository.Rollback();
                await _mediator.Send(new RemoveFromAzureInput()
                {
                    TipoUsuario = Constants.Roles.AUTOESCOLA,
                    Arquivos = fileList
                });

                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolCreateHandler),
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
