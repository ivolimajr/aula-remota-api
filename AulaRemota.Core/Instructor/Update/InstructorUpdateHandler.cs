using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Core.Services.Phone.UpdatePhone;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Instructor.Update
{
    public class InstructorUpdateHandler : IRequestHandler<InstructorUpdateInput, InstructorModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public InstructorUpdateHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            UnitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<InstructorModel> Handle(InstructorUpdateInput request, CancellationToken cancellationToken)
        {
            using var transaction = UnitOfWork.BeginTransaction();
            //Cria uma lista para receber os Files
            var fileList = new List<FileModel>();
            try
            {
                var instructorEntity = UnitOfWork.Instructor.Where(e => e.Id.Equals(request.Id))
                            .Include(e => e.PhonesNumbers)
                            .Include(e => e.Files)
                            .Include(e => e.User)
                            .Include(e => e.Address)
                            .FirstOrDefault();

                Check.NotNull(instructorEntity, "Não Encontrado");

                if (!string.IsNullOrWhiteSpace(request.Cpf) && !request.Cpf.Equals(instructorEntity.Cpf))
                    Check.NotExist(UnitOfWork.Instructor.Exists(e => e.Cpf == request.Cpf), "CNPJ já em uso.");
                if (!string.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(instructorEntity.Email))
                    Check.NotExist(UnitOfWork.Instructor.Exists(e => e.Email == request.Email), "Email estadual já em uso.");

                if (request.Birthdate >= DateTime.Today) throw new CustomException("Data da fundação inválida");
                if (request.Birthdate.Year > 1700 && request.Birthdate != instructorEntity.Birthdate) instructorEntity.Birthdate = request.Birthdate;

                instructorEntity.Name = request.Name ?? instructorEntity.Name;
                instructorEntity.Identity = request.Identity ?? instructorEntity.Identity;
                instructorEntity.Email = request.Email ?? instructorEntity.Email;
                instructorEntity.Birthdate = instructorEntity.Birthdate;
                instructorEntity.User.Email = request.Email ?? instructorEntity.Email;
                instructorEntity.User.UpdateAt = DateTime.Now;
                instructorEntity.Address.Cep = request.Cep ?? instructorEntity.Address.Cep;
                instructorEntity.Address.Uf = request.Uf ?? instructorEntity.Address.Uf;
                instructorEntity.Address.Address = request.FullAddress ?? instructorEntity.Address.Address;
                instructorEntity.Address.District = request.District ?? instructorEntity.Address.District;
                instructorEntity.Address.City = request.City ?? instructorEntity.Address.City;
                instructorEntity.Address.AddressNumber = request.AddressNumber ?? instructorEntity.Address.AddressNumber;
                instructorEntity.Address.Complement = request.Complement ?? instructorEntity.Address.Complement;

                if (Check.NotNull(request.PhonesNumbers))
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            instructorEntity.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            var res = await _mediator.Send(new PhoneUpdateInput
                            {
                                CurrentPhoneList = instructorEntity.PhonesNumbers,
                                RequestPhoneList = request.PhonesNumbers
                            }, cancellationToken);
                            Check.IsTrue(res, "Falha ao atualizar contato");
                        }
                    }

                if (Check.NotNull(request.Files))
                {
                    var fileResult = await _mediator.Send(new FileUploadToAzureInput
                    {
                        Files = request.Files,
                        TypeUser = Constants.Roles.INSTRUTOR
                    }, cancellationToken);
                    foreach (var item in fileResult.Files)
                    {
                        var arquivo = await UnitOfWork.File.AddAsync(item);
                        fileList.Add(item);
                    }
                    foreach (var item in fileList)
                        instructorEntity.Files.Add(item);
                }

                UnitOfWork.Address.Update(instructorEntity.Address);
                UnitOfWork.User.Update(instructorEntity.User);
                UnitOfWork.Instructor.Update(instructorEntity);
                UnitOfWork.Instructor.Save();
                transaction.Commit();

                return instructorEntity;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                await _mediator.Send(new RemoveFromAzureInput()
                {
                    TypeUser = Constants.Roles.INSTRUTOR,
                    Files = fileList
                }, cancellationToken);
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(InstructorUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
