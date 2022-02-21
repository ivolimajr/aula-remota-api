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
using System.Net;

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
            //Cria uma lista para receber os Files
            var fileList = new List<FileModel>();
            try
            {
                _autoEscolaRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                if (_usuarioRepository.Exists(u => u.Email == request.Email)) throw new CustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                if (_autoEscolaRepository.Exists(u => u.Cnpj == request.Cnpj)) throw new CustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.PhonesNumbers)
                    if (_telefoneRepository.Exists(u => u.PhoneNumber == item.PhoneNumber)) throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

                //CRIA UM USUÁRIO
                var user = new UserModel()
                {
                    Name = request.FantasyName.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
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
                    District = request.District.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    City = request.City.ToUpper(),
                    Address = request.Address.ToUpper(),
                    Number = request.Number.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                /*Faz o upload dos Files no azure e tem como retorno uma lista com os dados do upload
                 * @return nome, formato e destino
                 */
                var fileResult = await _mediator.Send(new FileUploadToAzureInput
                {
                    Files = request.Files,
                    TypeUser = Constants.Roles.AUTOESCOLA
                });

                //Salva no banco todas as informações dos Files do upload
                foreach (var item in fileResult.Files)
                {
                    var arquivo = await _arquivoRepository.CreateAsync(item);
                    fileList.Add(arquivo);
                }

                await _arquivoRepository.SaveChangesAsync();

                //CRIA UM EDRIVING
                var autoEscola = new DrivingSchoolModel()
                {
                    CorporateName = request.CorporateName.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Email = request.Email.ToUpper(),
                    PhonesNumbers = request.PhonesNumbers,
                    FoundingDate = request.FoundingDate,
                    Description = request.Description,
                    StateRegistration = request.StateRegistration,
                    FantasyName = request.FantasyName.ToUpper(),
                    Site = request.Site.ToUpper(),
                    User = user,
                    Address = address,
                    Files = fileList,
                };

                var autoEscolaModel = await _autoEscolaRepository.CreateAsync(autoEscola);

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _autoEscolaRepository.Commit();
                _autoEscolaRepository.Save();

                return new DrivingSchoolCreateResponse()
                {
                    Id = autoEscolaModel.Id,
                    CorporateName = autoEscolaModel.CorporateName.ToUpper(),
                    Cnpj = autoEscolaModel.Cnpj.ToUpper(),
                    Email = autoEscolaModel.Email.ToUpper(),
                    Description = autoEscolaModel.Description,
                    PhonesNumbers = autoEscolaModel.PhonesNumbers,
                    DataFundacao = autoEscolaModel.FoundingDate,
                    InscricaoEstadual = autoEscolaModel.StateRegistration,
                    FantasyName = autoEscolaModel.FantasyName,
                    Site = autoEscolaModel.Site,
                    User = autoEscolaModel.User,
                    Address = autoEscolaModel.Address,
                    Files = autoEscolaModel.Files
                };
            }
            catch (CustomException e)
            {
                _autoEscolaRepository.Rollback();
                await _mediator.Send(new RemoveFromAzureInput()
                {
                    TypeUser = Constants.Roles.AUTOESCOLA,
                    Files = fileList
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
