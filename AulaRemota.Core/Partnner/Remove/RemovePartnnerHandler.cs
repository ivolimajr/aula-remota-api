using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System;

namespace AulaRemota.Core.Partnner.Remove
{
    public class RemovePartnnerHandler : IRequestHandler<RemovePartnnerInput, bool>
    {
        private readonly IUnitOfWork UnitOfWork;

        public RemovePartnnerHandler(IUnitOfWork _unitOfWork) => UnitOfWork = _unitOfWork;

        public async Task<bool> Handle(RemovePartnnerInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            using var transaction = UnitOfWork.BeginTransaction();
            try
            {
                var partnner = await UnitOfWork.Partnner.Context
                                        .Set<PartnnerModel>()
                                        .Include(e => e.User)
                                        .Include(e => e.Address)
                                        .Include(e => e.PhonesNumbers)
                                        .Where(e => e.Id == request.Id)
                                        .FirstOrDefaultAsync();

                if (partnner == null) throw new CustomException("Não Encontrado");

                foreach (var item in partnner.PhonesNumbers)
                {
                    item.Edriving = null;
                    UnitOfWork.Phone.Delete(item);
                }
                UnitOfWork.SaveChanges();

                UnitOfWork.Address.Delete(partnner.Address);
                UnitOfWork.User.Delete(partnner.User);
                UnitOfWork.Partnner.Delete(partnner);

                UnitOfWork.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RemovePartnnerHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }
}
