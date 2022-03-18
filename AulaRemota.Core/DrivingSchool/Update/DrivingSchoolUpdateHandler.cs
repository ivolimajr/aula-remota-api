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
using AulaRemota.Core.Services.Phone.UpdatePhone;

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
                var drivingSchoolEntity = UnitOfWork.DrivingSchool.Where(e => e.Id.Equals(request.Id))
                            .Include(e => e.PhonesNumbers)
                            .Include(e => e.Files)
                            .Include(e => e.User)
                            .Include(e => e.Address)
                            .FirstOrDefault();

                Check.NotNull(drivingSchoolEntity, "Não Encontrado");


                if (!String.IsNullOrWhiteSpace(request.StateRegistration) && !request.StateRegistration.Equals(drivingSchoolEntity.StateRegistration))
                    if (UnitOfWork.DrivingSchool.Exists(e => e.StateRegistration == request.StateRegistration))
                        throw new CustomException("Inscrição estadual já em uso.");
                if (!String.IsNullOrWhiteSpace(request.Cnpj) && !request.Cnpj.Equals(drivingSchoolEntity.Cnpj))
                    if (UnitOfWork.DrivingSchool.Exists(e => e.Cnpj == request.Cnpj))
                        throw new CustomException("CNPJ já em uso.");
                if (!String.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(drivingSchoolEntity.Email))
                    if (UnitOfWork.DrivingSchool.Exists(e => e.Email == request.Email))
                        throw new CustomException("Email estadual já em uso.");

                if (request.FoundingDate >= DateTime.Today) throw new CustomException("Data da fundação inválida");
                if (request.FoundingDate.Year > 1700 && request.FoundingDate != drivingSchoolEntity.FoundingDate) drivingSchoolEntity.FoundingDate = request.FoundingDate;

                drivingSchoolEntity.CorporateName = request.CorporateName ?? drivingSchoolEntity.CorporateName;
                drivingSchoolEntity.FantasyName = request.FantasyName ?? drivingSchoolEntity.FantasyName;
                drivingSchoolEntity.Description = request.Description ?? drivingSchoolEntity.Description;
                drivingSchoolEntity.StateRegistration = request.StateRegistration ?? drivingSchoolEntity.StateRegistration;
                drivingSchoolEntity.Email = request.Email ?? drivingSchoolEntity.Email;
                drivingSchoolEntity.Cnpj = request.Cnpj ?? drivingSchoolEntity.Cnpj;
                drivingSchoolEntity.Site = request.Site ?? drivingSchoolEntity.Site;
                drivingSchoolEntity.FoundingDate = drivingSchoolEntity.FoundingDate;
                drivingSchoolEntity.User.Name = request.FantasyName ?? drivingSchoolEntity.FantasyName;
                drivingSchoolEntity.User.Email = request.Email ?? drivingSchoolEntity.Email;
                drivingSchoolEntity.User.UpdateAt = DateTime.Now;
                drivingSchoolEntity.Address.Cep = request.Cep ?? drivingSchoolEntity.Address.Cep;
                drivingSchoolEntity.Address.Uf = request.Uf ?? drivingSchoolEntity.Address.Uf;
                drivingSchoolEntity.Address.Address = request.FullAddress ?? drivingSchoolEntity.Address.Address;
                drivingSchoolEntity.Address.District = request.District ?? drivingSchoolEntity.Address.District;
                drivingSchoolEntity.Address.City = request.City ?? drivingSchoolEntity.Address.City;
                drivingSchoolEntity.Address.AddressNumber = request.AddressNumber ?? drivingSchoolEntity.Address.AddressNumber;
                drivingSchoolEntity.Address.Complement = request.Complement ?? drivingSchoolEntity.Address.Complement;

                if (Check.NotNull(request.PhonesNumbers))
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            drivingSchoolEntity.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            var res = await _mediator.Send(new PhoneUpdateInput
                            {
                                CurrentPhoneList = drivingSchoolEntity.PhonesNumbers,
                                RequestPhoneList = request.PhonesNumbers
                            });
                            if(!res) throw new CustomException("Falha ao atualizar contato");
                        }                        
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
                        drivingSchoolEntity.Files.Add(item);
                }

                UnitOfWork.Address.Update(drivingSchoolEntity.Address);
                UnitOfWork.User.Update(drivingSchoolEntity.User);
                UnitOfWork.DrivingSchool.Update(drivingSchoolEntity);
                UnitOfWork.DrivingSchool.Save();
                transaction.Commit();

                return drivingSchoolEntity;
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
