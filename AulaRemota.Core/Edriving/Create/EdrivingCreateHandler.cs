using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;

namespace AulaRemota.Core.Edriving.Create
{
    public class EdrivingCreateHandler : IRequestHandler<EdrivingCreateInput, EdrivingModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public EdrivingCreateHandler(IUnitOfWork _unitOfWork, IMediator mediator)
        {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }

        public async Task<EdrivingModel> Handle(EdrivingCreateInput request, CancellationToken cancellationToken)
        {
            using var transaction = UnitOfWork.BeginTransaction();
            try
            {
                RequestValidator(request.Email, request.Cpf, request.PhonesNumbers);
                var levelEntity = UnitOfWork.EdrivingLevel.Find(request.LevelId);
                Check.NotNull(levelEntity, "Cargo informado não existe");

                var edrivingModel = new EdrivingModel()
                {
                    Name = request.Name.ToUpper(),
                    Cpf = request.Cpf.ToUpper(),
                    Email = request.Email.ToUpper(),
                    LevelId = request.LevelId,
                    PhonesNumbers = request.PhonesNumbers,
                    Level = levelEntity,
                    User = new UserModel
                    {
                        Name = request.Name.ToUpper(),
                        Email = request.Email.ToUpper(),
                        Status = Constants.Status.ATIVO,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        Roles = new List<RolesModel>()
                            {
                            new RolesModel(){
                                    Role = Constants.Roles.EDRIVING
                                }
                            }
                    }
                };
                var edrivingResult = await UnitOfWork.Edriving.AddAsync(edrivingModel);
                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                UnitOfWork.SaveChanges();
                transaction.Commit();

                return edrivingResult;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingCreateResponse),
                    Exception = e,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
        private void RequestValidator(string email, string cpf, List<PhoneModel> phones)
        {
            Check.NotExist(UnitOfWork.User.Exists(u => u.Email == email), "Email já em uso");
            Check.NotExist(UnitOfWork.Edriving.Exists(u => u.Cpf == cpf), "Cpf já existe em nossa base de dados");
            foreach (var item in phones)
                Check.NotExist(UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber),"Telefone: " + item.PhoneNumber + " já em uso");
        }
    }
}