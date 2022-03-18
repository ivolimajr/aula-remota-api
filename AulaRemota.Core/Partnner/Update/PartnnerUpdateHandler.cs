using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System;
using AulaRemota.Core.Services.Phone.UpdatePhone;

namespace AulaRemota.Core.Partnner.Update
{
    public class PartnnerUpdateHandler : IRequestHandler<PartnnerUpdateInput, PartnnerModel>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public PartnnerUpdateHandler(IUnitOfWork _unitOfWork, IMediator mediator) {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }

        public async Task<PartnnerModel> Handle(PartnnerUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            using var transcation = UnitOfWork.BeginTransaction();
            try
            {
                //BUSCA O OBJETO A SER ATUALIZADO
                var partnnerEntity = await UnitOfWork.Partnner
                        .Where(e => e.Id == request.Id)
                        .Include(e => e.Level)
                        .Include(e => e.User)
                        .Include(e => e.Address)
                        .Include(e => e.PhonesNumbers)                        
                        .FirstOrDefaultAsync();

                Check.NotNull(partnnerEntity,"Não Encontrado");

                if (request.LevelId > 0 && !request.LevelId.Equals(partnnerEntity.LevelId))
                {
                    var levelEntity = UnitOfWork.PartnnerLevel.FirstOrDefault(e => e.Id.Equals(request.LevelId));
                    Check.NotNull(levelEntity, "Cargo Não Encontrado");
                    partnnerEntity.Level = levelEntity;
                }

                if (Check.NotNull(request.PhonesNumbers))
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            partnnerEntity.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            var res = await _mediator.Send(new PhoneUpdateInput
                            {
                                CurrentPhoneList = partnnerEntity.PhonesNumbers,
                                RequestPhoneList = request.PhonesNumbers
                            }, cancellationToken);
                            if (!res) throw new CustomException("Falha ao atualizar contato");
                        }
                    }

                partnnerEntity.Cnpj = request.Cnpj ?? partnnerEntity.Cnpj;
                partnnerEntity.Email = request.Email ?? partnnerEntity.Email;
                partnnerEntity.Name =  request.Name ?? partnnerEntity.Name;
                partnnerEntity.Description = request.Description ?? partnnerEntity.Description;

                partnnerEntity.Address.Uf = request.Address.Uf ?? partnnerEntity.Address.Uf;
                partnnerEntity.Address.Cep = request.Address.Cep ?? partnnerEntity.Address.Cep;
                partnnerEntity.Address.Address = request.Address.Address ?? partnnerEntity.Address.Address;
                partnnerEntity.Address.District = request.Address.District ?? partnnerEntity.Address.District;
                partnnerEntity.Address.City = request.Address.City ?? partnnerEntity.Address.City;
                partnnerEntity.Address.AddressNumber = request.Address.AddressNumber ?? partnnerEntity.Address.AddressNumber;

                partnnerEntity.User.Email = request.Email ?? partnnerEntity.Email;
                partnnerEntity.User.Name = request.Name ?? partnnerEntity.Name;

                partnnerEntity.LevelId = request.LevelId > 0 ? request.LevelId : partnnerEntity.LevelId;

                UnitOfWork.Partnner.Update(partnnerEntity);
                UnitOfWork.Partnner.Save();
                transcation.Commit();

                return partnnerEntity;
            }
            catch (Exception e)
            {
                transcation.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(PartnnerUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
