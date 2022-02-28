using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using AulaRemota.Shared.Helpers.Constants;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;

namespace AulaRemota.Core.DrivingSchool.Update
{
    public class DrivingSchoolUpdateHandler : IRequestHandler<DrivingSchoolUpdateInput, DrivingSchoolModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public DrivingSchoolUpdateHandler(IUnitOfWork _unitOfWork, IMediator mediator)
        {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }
        public async Task<DrivingSchoolModel> Handle(DrivingSchoolUpdateInput request, CancellationToken cancellationToken)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                //Cria uma lista para receber os Files
                var fileList = new List<FileModel>();
                try
                {
                    var drivingSchoolDb = UnitOfWork.DrivingSchool.Where(e => e.Id.Equals(request.Id))
                                .Include(e => e.PhonesNumbers).Include(e => e.Files).Include(e => e.User).Include(e => e.Address).FirstOrDefault();

                    if (drivingSchoolDb == null) throw new CustomException("Não Encontrado");

                    if (!String.IsNullOrWhiteSpace(request.CorporateName)) drivingSchoolDb.CorporateName = request.CorporateName;
                    if (!String.IsNullOrWhiteSpace(request.FantasyName))
                    {
                        drivingSchoolDb.FantasyName = request.FantasyName;
                        drivingSchoolDb.User.Name = request.FantasyName;
                    }
                    if (!String.IsNullOrWhiteSpace(request.Description)) drivingSchoolDb.Description = request.Description;
                    if (!String.IsNullOrWhiteSpace(request.Site)) drivingSchoolDb.Site = request.Site;
                    if (!String.IsNullOrWhiteSpace(request.Cep)) drivingSchoolDb.Address.Cep = request.Cep;
                    if (!String.IsNullOrWhiteSpace(request.Uf)) drivingSchoolDb.Address.Uf = request.Uf;
                    if (!String.IsNullOrWhiteSpace(request.Address)) drivingSchoolDb.Address.Address = request.Address;
                    if (!String.IsNullOrWhiteSpace(request.District)) drivingSchoolDb.Address.District = request.District;
                    if (!String.IsNullOrWhiteSpace(request.City)) drivingSchoolDb.Address.City = request.City;
                    if (!String.IsNullOrWhiteSpace(request.Number)) drivingSchoolDb.Address.Number = request.Number;
                    if (request.DataFundacao >= DateTime.Today) throw new CustomException("Data da fundação inválida");
                    if (request.DataFundacao.Year > 1700) drivingSchoolDb.FoundingDate = request.DataFundacao;


                    if (!String.IsNullOrWhiteSpace(request.StateRegistration) && !request.StateRegistration.Equals(drivingSchoolDb.StateRegistration))
                        if (UnitOfWork.DrivingSchool.Exists(e => e.StateRegistration == request.StateRegistration))
                            throw new CustomException("Inscrição estadual já em uso.");
                    if (!String.IsNullOrWhiteSpace(request.Cnpj) && !request.Cnpj.Equals(drivingSchoolDb.Cnpj))
                        if (UnitOfWork.DrivingSchool.Exists(e => e.Cnpj == request.Cnpj))
                            throw new CustomException("CNPJ já em uso.");
                    if (!String.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(drivingSchoolDb.Email))
                        if (UnitOfWork.DrivingSchool.Exists(e => e.Email == request.Email))
                            throw new CustomException("Email estadual já em uso.");

                    if (!String.IsNullOrWhiteSpace(request.StateRegistration)) drivingSchoolDb.StateRegistration = request.StateRegistration;
                    if (!String.IsNullOrWhiteSpace(request.Cnpj)) drivingSchoolDb.Cnpj = request.Cnpj;
                    if (!String.IsNullOrWhiteSpace(request.Email))
                    {
                        drivingSchoolDb.Email = request.Email;
                        drivingSchoolDb.User.Email = request.Email;
                    }

                    if (request.Files != null)
                    {
                        var fileResult = await _mediator.Send(new FileUploadToAzureInput
                        {
                            Files = request.Files,
                            TypeUser = Constants.Roles.AUTOESCOLA
                        });
                        //Salva no banco todas as informações dos Files do upload
                        foreach (var item in fileResult.Files)
                        {
                            var arquivo = await UnitOfWork.File.AddAsync(item);
                            fileList.Add(item);
                        }
                        foreach (var item in fileList)
                        {
                            drivingSchoolDb.Files.Add(item);
                        }
                    }

                    //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                    if (request.PhonesNumbers != null && request.PhonesNumbers.Count > 0)
                    {
                        foreach (var item in request.PhonesNumbers)
                        {
                            if (item.Id.Equals(0))
                            {
                                drivingSchoolDb.PhonesNumbers.Add(item);
                            }
                            else
                            {
                                if (!drivingSchoolDb.PhonesNumbers.Where(x => x.PhoneNumber.Equals(item.PhoneNumber)).Any())
                                {
                                    if (UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber))
                                        throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

                                    var phone = drivingSchoolDb.PhonesNumbers.Where(e => e.Id.Equals(item.Id)).FirstOrDefault();
                                    phone.PhoneNumber = item.PhoneNumber;
                                    UnitOfWork.Phone.Update(phone);
                                }

                            }
                        }
                    }

                    UnitOfWork.Address.Update(drivingSchoolDb.Address);
                    UnitOfWork.User.Update(drivingSchoolDb.User);
                    UnitOfWork.DrivingSchool.Update(drivingSchoolDb);
                    UnitOfWork.DrivingSchool.Save();
                    transaction.Commit();

                    return drivingSchoolDb;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    await _mediator.Send(new RemoveFromAzureInput()
                    {
                        TypeUser = Constants.Roles.AUTOESCOLA,
                        Files = fileList
                    });
                    throw new CustomException(new ResponseModel
                    {
                        UserMessage = e.Message,
                        ModelName = nameof(DrivingSchoolUpdateHandler),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }
            }
        }
    }
}
