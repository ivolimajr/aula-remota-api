using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System;
using AulaRemota.Core.Services.Phone.UpdatePhone;

namespace AulaRemota.Core.Edriving.Update
{
    public class EdrivingUpdateHandler : IRequestHandler<EdrivingUpdateInput, EdrivingModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;
        public EdrivingUpdateHandler(IUnitOfWork _unitOfWork, IMediator mediator)
        {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }

        public async Task<EdrivingModel> Handle(EdrivingUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            using var transaction = UnitOfWork.BeginTransaction();
            try
            {
                //BUSCA O OBJETO A SER ATUALIZADO
                var edrivingEntity = UnitOfWork.Edriving
                            .Where(e => e.Id == request.Id)
                            .Include(e => e.User)
                            .Include(e => e.Level)
                            .Include(e => e.PhonesNumbers)
                            .FirstOrDefault();

                Check.NotNull(edrivingEntity, "Não Encontrado");

                if (string.IsNullOrEmpty(request.Email) && request.Email != edrivingEntity.Email)
                    if (UnitOfWork.User.Exists(e => e.Email.Equals(request.Email)))
                        throw new CustomException("Email já em uso");

                if (!string.IsNullOrEmpty(request.Cpf) && !request.Cpf.Equals(edrivingEntity.Cpf))
                {
                    var cpfUnique = UnitOfWork.Edriving.FirstOrDefault(u => u.Cpf == request.Cpf);
                    if (cpfUnique != null && cpfUnique.Id != request.Id) throw new CustomException("Cpf já existe em nossa base de dados");
                }

                if (request.LevelId > 0 && !request.LevelId.Equals(edrivingEntity.LevelId))
                {
                    var level = UnitOfWork.EdrivingLevel.FirstOrDefault(e => e.Id.Equals(request.LevelId));
                    if (level == null) throw new CustomException("Cargo Não Encontrado");
                    edrivingEntity.Level = level;
                }

                edrivingEntity.Cpf = request.Cpf ?? edrivingEntity.Cpf;
                edrivingEntity.Email = request.Email ?? edrivingEntity.Email.ToUpper();
                edrivingEntity.Name = request.Name ?? edrivingEntity.Name.ToUpper();
                edrivingEntity.User.Email = request.Email ?? edrivingEntity.Email.ToUpper();
                edrivingEntity.User.Name = request.Name ?? edrivingEntity.Name.ToUpper();
                edrivingEntity.LevelId = request.LevelId > 0 ? request.LevelId : edrivingEntity.LevelId;

                if (Check.NotNull(request.PhonesNumbers))
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            edrivingEntity.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            var res = await _mediator.Send(new PhoneUpdateInput
                            {
                                CurrentPhoneList = edrivingEntity.PhonesNumbers,
                                RequestPhoneList = request.PhonesNumbers
                            }, cancellationToken);
                            if (!res) throw new CustomException("Falha ao atualizar contato");
                        }
                    }

                UnitOfWork.Edriving.Update(edrivingEntity);

                UnitOfWork.Edriving.SaveChanges();
                transaction.Commit();

                return edrivingEntity;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}