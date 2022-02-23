using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;

namespace AulaRemota.Core.DrivingSchool.Create
{
    public class DrivingSchoolCreateHandler : IRequestHandler<DrivingSchoolCreateInput, DrivingSchoolCreateResponse>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public DrivingSchoolCreateHandler(
            IUnitOfWork _unitOfWork,
            IMediator mediator
            )
        {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }
        public async Task<DrivingSchoolCreateResponse> Handle(DrivingSchoolCreateInput request, CancellationToken cancellationToken)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                //Cria uma lista para receber os Files
                var fileList = new List<FileModel>();
                try
                {
                    if (UnitOfWork.User.Exists(u => u.Email == request.Email)) throw new CustomException("Email já em uso");
                    if (UnitOfWork.DrivingSchool.Exists(u => u.Cnpj == request.Cnpj)) throw new CustomException("Cnpj já existe em nossa base de dados");

                    foreach (var item in request.PhonesNumbers)
                        if (UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber)) throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

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
                        var arquivo = await UnitOfWork.File.AddAsync(item);
                        fileList.Add(arquivo);
                    }

                    var autoEscola = new DrivingSchoolModel()
                    {
                        CorporateName = request.CorporateName.ToUpper(),
                        Cnpj = request.Cnpj.ToUpper(),
                        Email = request.Email.ToUpper(),
                        FoundingDate = request.FoundingDate,
                        Description = request.Description,
                        StateRegistration = request.StateRegistration,
                        FantasyName = request.FantasyName.ToUpper(),
                        Site = request.Site.ToUpper(),
                        PhonesNumbers = request.PhonesNumbers,
                        User = new UserModel
                        {
                            Name = request.FantasyName.ToUpper(),
                            Email = request.Email.ToUpper(),
                            Status = 1,
                            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                            Roles = new List<RolesModel>()
                            {
                                new RolesModel
                                {
                                    Role = Constants.Roles.AUTOESCOLA
                                }
                            }
                        },
                        Address = new AddressModel
                        {
                            District = request.District.ToUpper(),
                            Cep = request.Cep.ToUpper(),
                            City = request.City.ToUpper(),
                            Address = request.Address.ToUpper(),
                            Number = request.Number.ToUpper(),
                            Uf = request.Uf.ToUpper(),
                        },
                        Files = fileList,
                    };
                    var autoEscolaModel = await UnitOfWork.DrivingSchool.AddAsync(autoEscola);
                    UnitOfWork.SaveChanges();
                    transaction.Commit();

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
                    await _mediator.Send(new RemoveFromAzureInput()
                    {
                        TypeUser = Constants.Roles.AUTOESCOLA,
                        Files = fileList
                    });
                    transaction.Rollback();

                    throw new CustomException(new ResponseModel
                    {
                        UserMessage = e.Message,
                        ModelName = nameof(DrivingSchoolCreateHandler),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = e.ResponseModel.StatusCode
                    });
                }
            }
        }
    }
}
