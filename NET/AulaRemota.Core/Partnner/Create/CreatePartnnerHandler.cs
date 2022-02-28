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
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                    bool emailResult = UnitOfWork.User.Exists(u => u.Email == request.Email);
                    if (emailResult) throw new CustomException("Email já em uso");

                    //VERIFICA SE O CPF JÁ ESTÁ EM USO
                    bool cpfResult = UnitOfWork.Partnner.Exists(u => u.Cnpj == request.Cnpj);
                    if (cpfResult) throw new CustomException("Cnpj já existe em nossa base de dados");

                    //VERIFICA SE O Level INFORMADO EXISTE
                    var Level = UnitOfWork.PartnnerLevel.Find(request.LevelId);
                    if (Level == null) throw new CustomException("Cargo informado não existe");

                    //VERIFICA SE O CPF JÁ ESTÁ EM USO
                    foreach (var item in request.PhonesNumbers)
                    {
                        var phoneResult = await UnitOfWork.Phone.FirstOrDefaultAsync(u => u.PhoneNumber == item.PhoneNumber);
                        if (phoneResult != null) throw new CustomException("Telefone: " + phoneResult.PhoneNumber + " já em uso");
                    }

                    //CRIA UM PARCEIRO
                    var partnner = new PartnnerModel()
                    {
                        Name = request.Name.ToUpper(),
                        Cnpj = request.Cnpj.ToUpper(),
                        Description = request.Description.ToUpper(),
                        Email = request.Email.ToUpper(),
                        PhonesNumbers = request.PhonesNumbers,
                        LevelId = request.LevelId,
                        Level = Level,
                        Address = new AddressModel
                        {
                            District = request.District.ToUpper(),
                            Cep = request.Cep.ToUpper(),
                            City = request.City.ToUpper(),
                            Address = request.Address.ToUpper(),
                            Number = request.Number.ToUpper(),
                            Uf = request.Uf.ToUpper(),
                        },
                        User = new UserModel
                        {
                            Name = request.Name.ToUpper(),
                            Email = request.Email.ToUpper(),
                            Status = 1,
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

                    var partnnerResult = UnitOfWork.Partnner.Add(partnner);
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
        }
    }
}
