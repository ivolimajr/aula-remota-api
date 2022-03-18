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
            using var transaction = UnitOfWork.BeginTransaction();
            //Cria uma lista para receber os Files
            var fileList = new List<FileModel>();
            try
            {
                var drivingSchoolDb = UnitOfWork.DrivingSchool.Where(e => e.Id.Equals(request.Id))
                            .Include(e => e.PhonesNumbers)
                            .Include(e => e.Files)
                            .Include(e => e.User)
                            .Include(e => e.Address)
                            .FirstOrDefault();

                Check.NotNull(drivingSchoolDb, "Não Encontrado");


                if (!String.IsNullOrWhiteSpace(request.StateRegistration) && !request.StateRegistration.Equals(drivingSchoolDb.StateRegistration))
                    if (UnitOfWork.DrivingSchool.Exists(e => e.StateRegistration == request.StateRegistration))
                        throw new CustomException("Inscrição estadual já em uso.");
                if (!String.IsNullOrWhiteSpace(request.Cnpj) && !request.Cnpj.Equals(drivingSchoolDb.Cnpj))
                    if (UnitOfWork.DrivingSchool.Exists(e => e.Cnpj == request.Cnpj))
                        throw new CustomException("CNPJ já em uso.");
                if (!String.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(drivingSchoolDb.Email))
                    if (UnitOfWork.DrivingSchool.Exists(e => e.Email == request.Email))
                        throw new CustomException("Email estadual já em uso.");

                if (request.FoundingDate >= DateTime.Today) throw new CustomException("Data da fundação inválida");
                if (request.FoundingDate.Year > 1700 && request.FoundingDate != drivingSchoolDb.FoundingDate) drivingSchoolDb.FoundingDate = request.FoundingDate;

                DrivingSchoolModel model = new()
                {
                    Id = request.Id,
                    CorporateName = request.CorporateName ?? drivingSchoolDb.CorporateName,
                    FantasyName = request.FantasyName ?? drivingSchoolDb.FantasyName,
                    Description = request.Description ?? drivingSchoolDb.Description,
                    StateRegistration = request.StateRegistration ?? drivingSchoolDb.StateRegistration,
                    Email = request.Email ?? drivingSchoolDb.Email,
                    Cnpj = request.Cnpj ?? drivingSchoolDb.Cnpj,
                    Site = request.Site ?? drivingSchoolDb.Site,
                    FoundingDate = drivingSchoolDb.FoundingDate,
                    UserId = drivingSchoolDb.UserId,
                    User = new()
                    {
                        Id = drivingSchoolDb.UserId,
                        Name = request.FantasyName ?? drivingSchoolDb.FantasyName,
                        Email = request.Email ?? drivingSchoolDb.Email,
                        Password = drivingSchoolDb.User.Password,
                        UpdateAt = DateTime.Now
                    },
                    AddressId = drivingSchoolDb.AddressId,
                    Address = new()
                    {
                        Id = drivingSchoolDb.AddressId,
                        Cep = request.Cep ?? drivingSchoolDb.Address.Cep,
                        Uf = request.Uf ?? drivingSchoolDb.Address.Uf,
                        Address = request.FullAddress ?? drivingSchoolDb.Address.Address,
                        District = request.District ?? drivingSchoolDb.Address.District,
                        City = request.City ?? drivingSchoolDb.Address.City,
                        AddressNumber = request.AddressNumber ?? drivingSchoolDb.Address.AddressNumber,
                        Complement = request.Complement ?? drivingSchoolDb.Address.Complement
                    },
                    Files = drivingSchoolDb.Files,
                    PhonesNumbers = drivingSchoolDb.PhonesNumbers
                };

                if (Check.NotNull(request.PhonesNumbers))
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            model.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            if (UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber))
                                throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

                            var phone = model.PhonesNumbers.Where(e => e.Id.Equals(item.Id)).FirstOrDefault();
                            phone.PhoneNumber = item.PhoneNumber;
                            UnitOfWork.Phone.Update(phone);
                        }
                        await UnitOfWork.Phone.SaveChangesAsync();
                    }

                if (Check.NotNull(request.Files))
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
                        model.Files.Add(item);
                }

                UnitOfWork.Address.Update(model.Address);
                UnitOfWork.User.Update(model.User);
                UnitOfWork.DrivingSchool.Update(model);
                UnitOfWork.DrivingSchool.Save();
                transaction.Commit();

                return model;
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
