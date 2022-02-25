using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;

namespace AulaRemota.Core.Edriving.Update
{
    public class EdrivingUpdateHandler : IRequestHandler<EdrivingUpdateInput, EdrivingModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        public EdrivingUpdateHandler(IUnitOfWork _unitOfWork) => UnitOfWork = _unitOfWork;

        public async Task<EdrivingModel> Handle(EdrivingUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    //BUSCA O OBJETO A SER ATUALIZADO
                    var edriving = UnitOfWork.Context
                                .Set<EdrivingModel>()
                                .Include(e => e.User)
                                .Include(e => e.Level)
                                .Include(e => e.PhonesNumbers)
                                .Where(e => e.Id == request.Id)
                                .FirstOrDefault();

                    if (edriving == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                    if (!string.IsNullOrEmpty(request.Email) && !request.Email.Equals(edriving.Email))
                    {
                        var emailUnique = UnitOfWork.User.FirstOrDefault(u => u.Email == request.Email);
                        if (emailUnique != null && emailUnique.Id != request.Id) throw new CustomException("Email em uso");
                    }
                    if (!string.IsNullOrEmpty(request.Cpf) && !request.Cpf.Equals(edriving.Cpf))
                    {
                        var cpfUnique = UnitOfWork.Edriving.FirstOrDefault(u => u.Cpf == request.Cpf);
                        if (cpfUnique != null && cpfUnique.Id != request.Id) throw new CustomException("Cpf já existe em nossa base de dados");
                    }

                    if (request.LevelId > 0 && !request.LevelId.Equals(edriving.LevelId))
                    {
                        var level = UnitOfWork.EdrivingLevel.FirstOrDefault(e => e.Id.Equals(request.LevelId));
                        if (level == null) throw new CustomException("Level Não Encontrado", HttpStatusCode.NotFound);
                        edriving.Level = level;
                    }

                    edriving.Cpf = !string.IsNullOrEmpty(request.Cpf) ? request.Cpf : edriving.Cpf;
                    edriving.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.ToUpper() : edriving.Email.ToUpper();
                    edriving.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.ToUpper() : edriving.Name.ToUpper();
                    edriving.User.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.ToUpper() : edriving.Email.ToUpper();
                    edriving.User.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.ToUpper() : edriving.Name.ToUpper();
                    edriving.LevelId = request.LevelId > 0 ? request.LevelId : edriving.LevelId;

                    //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                    if (request.PhonesNumbers != null && request.PhonesNumbers.Count > 0)
                        foreach (var item in request.PhonesNumbers)
                        {
                            if (item.Id.Equals(0))
                            {
                                edriving.PhonesNumbers.Add(item);
                            }
                            else
                            {
                                if (!UnitOfWork.Phone.Where(x => x.PhoneNumber.Equals(item.PhoneNumber)).Any())
                                {
                                    if (UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber))
                                        throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

                                    var phone = edriving.PhonesNumbers.Where(e => e.Id.Equals(item.Id)).FirstOrDefault();
                                    phone.PhoneNumber = item.PhoneNumber;
                                    UnitOfWork.Phone.Update(phone);
                                }
                            }
                        }

                    UnitOfWork.Edriving.Update(edriving);

                    UnitOfWork.Edriving.SaveChanges();
                    transaction.Commit();

                    return edriving;
                }
                catch (CustomException e)
                {
                    transaction.Rollback();
                    throw new CustomException(new ResponseModel
                    {
                        UserMessage = e.Message,
                        ModelName = nameof(EdrivingUpdateHandler),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = e.ResponseModel.StatusCode
                    });
                }
            }
        }
    }
}