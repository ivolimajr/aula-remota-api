using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerHandler : IRequestHandler<CreatePartnnerInput, PartnnerModel>
    {
        private readonly IUnitOfWork UnitOfWork;

        public CreatePartnnerHandler(IUnitOfWork _unitOfWork) => UnitOfWork = _unitOfWork;

        public async Task<PartnnerModel> Handle(CreatePartnnerInput request, CancellationToken cancellationToken)
        {
            using var transaction = UnitOfWork.BeginTransaction();
            try
            {
                RequestValidator(request.Email, request.Cnpj, request.PhonesNumbers);

                var levelEntity = UnitOfWork.PartnnerLevel.Find(request.LevelId);
                Check.NotNull(levelEntity, "Cargo informado não existe");

                foreach (var item in request.PhonesNumbers)
                    Check.NotExist(UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber), "Telefone: " + item.PhoneNumber + " já em uso");

                var partnnerModel = new PartnnerModel()
                {
                    Name = request.Name.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Description = request.Description.ToUpper(),
                    Email = request.Email.ToUpper(),
                    PhonesNumbers = request.PhonesNumbers,
                    LevelId = request.LevelId,
                    Level = levelEntity,
                    Address = request.Address,
                    User = new UserModel
                    {
                        Name = request.Name.ToUpper(),
                        Email = request.Email.ToUpper(),
                        Status = Constants.Status.ATIVO,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        Roles = new List<RolesModel>()
                            {
                                new RolesModel()
                                {
                                    Role = Constants.Roles.PARCEIRO
                                }
                            }
                    }
                };

                var partnnerResult = UnitOfWork.Partnner.Add(partnnerModel);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();
                return partnnerResult;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(CreatePartnnerInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        private void RequestValidator(string email, string cnpj, List<PhoneModel> phoneList)
        {
            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            bool emailResult = UnitOfWork.User.Exists(u => u.Email == email);
            if (emailResult) throw new CustomException("Email já em uso");

            //VERIFICA SE O CPF JÁ ESTÁ EM USO
            bool cpfResult = UnitOfWork.Partnner.Exists(u => u.Cnpj == cnpj);
            if (cpfResult) throw new CustomException("Cnpj já existe em nossa base de dados");

            Check.NotNull(phoneList, "Lista de telefones vazia");
        }
    }
}
