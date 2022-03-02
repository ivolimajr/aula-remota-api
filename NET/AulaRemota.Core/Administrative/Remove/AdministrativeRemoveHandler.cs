using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.Remove
{
    public class AdministrativeRemoveHandler : IRequestHandler<AdministrativeRemoveInput, bool>
    {
        private readonly IUnitOfWork UnitOfWork;

        public AdministrativeRemoveHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AdministrativeRemoveInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Não encontrado");

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    var administrative = UnitOfWork.Administrative
                                    .Where(e => e.Id.Equals(request.Id))
                                    .Include(e => e.Address)
                                    .Include(e => e.User)
                                    .Include(e => e.PhonesNumbers)
                                    .FirstOrDefault();

                    if (administrative == null) throw new CustomException("Não encontrado");
                    
                    foreach (var item in administrative.PhonesNumbers)
                        UnitOfWork.Phone.Delete(item);
                    await UnitOfWork.SaveChangesAsync();

                    UnitOfWork.User.Delete(administrative.User);
                    UnitOfWork.Address.Delete(administrative.Address);
                    UnitOfWork.Administrative.Delete(administrative);

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
                        ModelName = nameof(AdministrativeRemoveInput),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = HttpStatusCode.NotFound
                    });
                }
            }

        }
    }
}
