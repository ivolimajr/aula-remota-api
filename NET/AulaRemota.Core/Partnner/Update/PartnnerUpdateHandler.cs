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

namespace AulaRemota.Core.Partnner.Update
{
    public class PartnnerUpdateHandler : IRequestHandler<PartnnerUpdateInput, PartnnerUpdateResponse>
    {
        private readonly IUnitOfWork UnitOfWork;

        public PartnnerUpdateHandler(IUnitOfWork _unitOfWork) => UnitOfWork = _unitOfWork;

        public async Task<PartnnerUpdateResponse> Handle(PartnnerUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            using (var transcation = UnitOfWork.BeginTransaction())
            {
                try
                {
                    //BUSCA O OBJETO A SER ATUALIZADO
                    var partnner = await UnitOfWork.Partnner.Context
                            .Set<PartnnerModel>()
                            .Include(e => e.Level)
                            .Include(e => e.User)
                            .Include(e => e.Address)
                            .Include(e => e.PhonesNumbers)
                            .Where(e => e.Id == request.Id)
                            .FirstOrDefaultAsync();

                    if (partnner == null) throw new CustomException("Não Encontrado");

                    if (request.LevelId > 0 && !request.LevelId.Equals(partnner.LevelId))
                    {
                        var level = UnitOfWork.PartnnerLevel.FirstOrDefault(e => e.Id.Equals(request.LevelId));
                        if (level == null) throw new CustomException("Level Não Encontrado");
                        partnner.Level = level;
                    }

                    //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                    if (request.PhonesNumbers != null && request.PhonesNumbers.Count > 0)
                        foreach (var item in request.PhonesNumbers)
                        {
                            if (item.Id.Equals(0))
                            {
                                partnner.PhonesNumbers.Add(item);
                            }
                            else
                            {
                                if (!UnitOfWork.Phone.Where(x => x.PhoneNumber.Equals(item.PhoneNumber)).Any())
                                {
                                    if (UnitOfWork.Phone.Exists(u => u.PhoneNumber == item.PhoneNumber))
                                        throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

                                    var phone = partnner.PhonesNumbers.Where(e => e.Id.Equals(item.Id)).FirstOrDefault();
                                    phone.PhoneNumber = item.PhoneNumber;
                                    UnitOfWork.Phone.Update(phone);
                                }
                            }
                        }

                    partnner.Cnpj = !string.IsNullOrEmpty(request.Cnpj) ? request.Cnpj : partnner.Cnpj;
                    partnner.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.ToUpper() : partnner.Email.ToUpper();
                    partnner.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.ToUpper() : partnner.Name.ToUpper();
                    partnner.Description = !string.IsNullOrEmpty(request.Description) ? request.Description.ToUpper() : partnner.Description.ToUpper();

                    partnner.Address.Uf = !string.IsNullOrEmpty(request.Address.Uf) ? request.Address.Uf.ToUpper() : partnner.Address.Uf.ToUpper();
                    partnner.Address.Cep = !string.IsNullOrEmpty(request.Address.Cep) ? request.Address.Cep.ToUpper() : partnner.Address.Cep.ToUpper();
                    partnner.Address.Address = !string.IsNullOrEmpty(request.Address.Address) ? request.Address.Address.ToUpper() : partnner.Address.Address.ToUpper();
                    partnner.Address.District = !string.IsNullOrEmpty(request.Address.District) ? request.Address.District.ToUpper() : partnner.Address.District.ToUpper();
                    partnner.Address.City = !string.IsNullOrEmpty(request.Address.City) ? request.Address.City.ToUpper() : partnner.Address.City.ToUpper();
                    partnner.Address.AddressNumber = !string.IsNullOrEmpty(request.Address.AddressNumber) ? request.Address.AddressNumber.ToUpper() : partnner.Address.AddressNumber.ToUpper();

                    partnner.User.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.ToUpper() : partnner.Email.ToUpper();
                    partnner.User.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.ToUpper() : partnner.Name.ToUpper();

                    partnner.LevelId = request.LevelId > 0 ? request.LevelId : partnner.LevelId;

                    UnitOfWork.Partnner.Update(partnner);
                    UnitOfWork.Partnner.Save();
                    transcation.Commit();

                    return new PartnnerUpdateResponse
                    {
                        Id = partnner.Id,
                        Name = partnner.Name,
                        Email = partnner.Email,
                        Cnpj = partnner.Cnpj,
                        Description = partnner.Description,
                        PhonesNumbers = partnner.PhonesNumbers,
                        LevelId = partnner.LevelId,
                        UserId = partnner.UserId,
                        Level = partnner.Level,
                        User = partnner.User,
                        AddressId = partnner.AddressId,
                        Address = partnner.Address
                    };
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
}
