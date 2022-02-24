using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;

namespace AulaRemota.Core.Edriving.Remove
{
    public class EdrivingRemoveHandler : IRequestHandler<EdrivingRemoveInput, bool>
    {
        private readonly IUnitOfWork UnitOfWork;
        public EdrivingRemoveHandler(
            IUnitOfWork _unitOfWork
            )
        {
            UnitOfWork = _unitOfWork;
        }

        public async Task<bool> Handle(EdrivingRemoveInput request, CancellationToken cancellationToken)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                if (request.Id == 0) throw new CustomException("Busca Inválida");
                try
                {

                    var edriving = await UnitOfWork.Edriving.Context
                                            .Set<EdrivingModel>()
                                            .Include(e => e.User)
                                            .Include(e => e.PhonesNumbers)
                                            .Where(e => e.Id == request.Id)
                                            .FirstOrDefaultAsync();
                    if (edriving == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                    foreach (var item in edriving.PhonesNumbers)
                    {
                        UnitOfWork.Phone.Delete(item);
                    }
                    UnitOfWork.SaveChanges();

                    UnitOfWork.User.Delete(edriving.User);
                    UnitOfWork.Edriving.Delete(edriving);

                    UnitOfWork.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (CustomException e)
                {
                    transaction.Rollback();
                    throw new CustomException(new ResponseModel
                    {
                        UserMessage = e.Message,
                        ModelName = nameof(EdrivingRemoveInput),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = e.ResponseModel.StatusCode
                    });
                }
            }
        }
    }
}
