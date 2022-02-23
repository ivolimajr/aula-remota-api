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
    public class EdrivingCreateHandler : IRequestHandler<EdrivingCreateInput, EdrivingCreateResponse>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public EdrivingCreateHandler(
            IUnitOfWork _unitOfWork,
            IMediator mediator
            )
        {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }

        public async Task<EdrivingCreateResponse> Handle(EdrivingCreateInput request, CancellationToken cancellationToken)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                    var emailResult = await UnitOfWork.User.FirstOrDefaultAsync(u => u.Email == request.Email);
                    if (emailResult != null) throw new CustomException("Email já em uso");

                    //VERIFICA SE O CPF JÁ ESTÁ EM USO
                    var cpfResult = await UnitOfWork.Edriving.FirstOrDefaultAsync(u => u.Cpf == request.Cpf);
                    if (cpfResult != null) throw new CustomException("Cpf já existe em nossa base de dados");
                    //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                    if (request.PhonesNumbers != null)
                    {
                        foreach (var item in request.PhonesNumbers)
                        {
                            var telefoneResult = await UnitOfWork.Phone.FirstOrDefaultAsync(u => u.PhoneNumber == item.PhoneNumber);
                            if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.PhoneNumber + " já em uso");
                        }
                    }
                    //VERIFICA SE O Level INFORMADO EXISTE
                    var Level = UnitOfWork.EdrivingLevel.Find(request.LevelId);
                    if (Level == null) throw new CustomException("Level informado não existe");

                    //CRIA UM EDRIVING
                    var edriving = new EdrivingModel()
                    {
                        Name = request.Name.ToUpper(),
                        Cpf = request.Cpf.ToUpper(),
                        Email = request.Email.ToUpper(),
                        LevelId = request.LevelId,
                        PhonesNumbers = request.PhonesNumbers,
                        Level = Level,
                        User = new UserModel
                        {
                            Name = request.Name.ToUpper(),
                            Email = request.Email.ToUpper(),
                            Status = 1,
                            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                            Roles = new List<RolesModel>()
                            {
                            new RolesModel(){
                                    Role = Constants.Roles.EDRIVING
                                }
                            }
                        }
                    };
                    var edrivingModel = await UnitOfWork.Edriving.AddAsync(edriving);
                    //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                    UnitOfWork.SaveChanges();
                    transaction.Commit();

                    return new EdrivingCreateResponse()
                    {
                        Id = edrivingModel.Id,
                        Name = edrivingModel.Name,
                        Email = edrivingModel.Email,
                        Cpf = edrivingModel.Cpf,
                        PhonesNumbers = edrivingModel.PhonesNumbers,
                        LevelId = edrivingModel.LevelId,
                        UserId = edrivingModel.UserId,
                        Level = edrivingModel.Level,
                        User = edrivingModel.User,
                    };
                }
                catch (CustomException e)
                {
                    transaction.Rollback();
                    throw new CustomException(new ResponseModel
                    {
                        UserMessage = e.Message,
                        ModelName = nameof(EdrivingCreateResponse),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }
            }
        }
    }
}