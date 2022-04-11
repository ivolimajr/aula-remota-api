using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Instructor.Create
{
    public class InstructorCreateHandler : IRequestHandler<InstructorCreateInput, InstructorModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public InstructorCreateHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            UnitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<InstructorModel> Handle(InstructorCreateInput request, CancellationToken cancellationToken)
        {
            Check.NotNull(request.Files, "Documentos ausentes");

            using var transaction = UnitOfWork.BeginTransaction();
            //Cria uma lista para receber os Files
            var fileList = new List<FileModel>();
            try
            {
                ValidateRequest(request.Email, request.Cpf, request.PhonesNumbers.ToList());
                /*Faz o upload dos Files no azure e tem como retorno uma lista com os dados do upload
                 * @return nome, formato e destino
                 */
                var fileResult = await _mediator.Send(new FileUploadToAzureInput
                {
                    Files = request.Files,
                    TypeUser = Constants.Roles.INSTRUTOR
                }, cancellationToken);
                Check.NotNull(fileResult, "Lista de arquivos ausente");

                //Salva no banco todas as informações dos Files do upload
                foreach (var item in fileResult.Files)
                {
                    var file = await UnitOfWork.File.AddAsync(item);
                    fileList.Add(file);
                }

                var instructorModel = new InstructorModel()
                {
                    Name = request.Name.ToUpper(),
                    Cpf = request.Cpf.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Identity = request.Identity,
                    Origin = request.Origin.ToUpper(),
                    Birthdate = request.Birthdate,
                    PhonesNumbers = request.PhonesNumbers,
                    User = new UserModel
                    {
                        Name = request.Name.ToUpper(),
                        Email = request.Email.ToUpper(),
                        Status = Constants.Status.ATIVO,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        Roles = new List<RolesModel>()
                            {
                                new RolesModel
                                {
                                    Role = Constants.Roles.INSTRUTOR
                                }
                            }
                    },
                    Address = new AddressModel
                    {
                        District = request.District.ToUpper(),
                        Cep = request.Cep.ToUpper(),
                        City = request.City.ToUpper(),
                        Address = request.FullAddress.ToUpper(),
                        AddressNumber = request.AddressNumber.ToUpper(),
                        Complement = request.Complement,
                        Uf = request.Uf.ToUpper(),
                    },
                    Files = fileList,
                };
                var drivingSchoolResult = await UnitOfWork.Instructor.AddAsync(instructorModel);
                UnitOfWork.SaveChanges();
                transaction.Commit();

                return drivingSchoolResult;
            }
            catch (Exception e)
            {
                await _mediator.Send(new RemoveFromAzureInput()
                {
                    TypeUser = Constants.Roles.INSTRUTOR,
                    Files = fileList
                }, cancellationToken);
                transaction.Rollback();

                object result = new
                {
                    name = request.Name,
                    email = request.Email,
                    cpf = request.Cpf,
                    identity = request.Identity,
                    origin = request.Origin,
                    birthDate = request.Birthdate,
                    phones = request.PhonesNumbers,
                    district = request.District,
                    cep = request.Cep,
                    address = request.FullAddress,
                    city = request.City,
                    complement = request.Complement,
                    uf = request.Uf,
                    files = request.Files
                };

                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(InstructorCreateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = result
                });
            }
            throw new NotImplementedException();
        }
        private void ValidateRequest(string email, string cnpj, List<PhoneModel> phoneList)
        {
            if (UnitOfWork.User.Exists(u => u.Email == email)) throw new CustomException("Email já em uso");
            if (UnitOfWork.DrivingSchool.Exists(u => u.Cnpj == cnpj)) throw new CustomException("Cnpj já existe em nossa base de dados");

            foreach (var item in phoneList)
                if (UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber)) throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");
        }
    }
}
