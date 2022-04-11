using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System.Collections.Generic;
using System;

namespace AulaRemota.Core.DrivingSchool.Remove
{
    public class DrivingSchoolRemoveHandler : IRequestHandler<DrivingSchoolRemoveInput, bool>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public DrivingSchoolRemoveHandler(IUnitOfWork _unitOfWork, IMediator mediator)
        {
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DrivingSchoolRemoveInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            using var transaction = UnitOfWork.BeginTransaction();
            var fileList = new List<FileModel>();
            try
            {
                var drivingSchoolEntity = await UnitOfWork.DrivingSchool.Context
                    .Set<DrivingSchoolModel>()
                    .Include(e => e.User)
                    .Include(e => e.PhonesNumbers)
                    .Include(e => e.Address)
                    .Include(e => e.Files)
                    .Include(e => e.Administratives).ThenInclude(e => e.User)
                    .Include(e => e.Administratives).ThenInclude(e => e.Address)
                    .Include(e => e.Administratives).ThenInclude(e => e.PhonesNumbers)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                Check.NotNull(drivingSchoolEntity, "Não encontrado");
                Check.NotNull(drivingSchoolEntity.PhonesNumbers, "Problemas ao remover lista de telefones");
                Check.NotNull(drivingSchoolEntity.Files, "Problemas ao remover lista de arquivos");

                foreach (var item in drivingSchoolEntity.PhonesNumbers)
                    UnitOfWork.Phone.Delete(item);

                fileList = drivingSchoolEntity.Files.ToList();
                UnitOfWork.SaveChanges();

                foreach (var item in drivingSchoolEntity.Files)
                    UnitOfWork.File.Delete(item);
                UnitOfWork.SaveChanges();

                foreach (var administrative in drivingSchoolEntity.Administratives)
                {
                    foreach (var phone in administrative.PhonesNumbers)
                        UnitOfWork.Phone.Delete(phone);

                    await UnitOfWork.SaveChangesAsync();
                    UnitOfWork.User.Delete(administrative.User);
                    UnitOfWork.Address.Delete(administrative.Address);
                    UnitOfWork.Administrative.Delete(administrative);
                }

                UnitOfWork.User.Delete(drivingSchoolEntity.User);
                UnitOfWork.Address.Delete(drivingSchoolEntity.Address);
                UnitOfWork.DrivingSchool.Delete(drivingSchoolEntity);

                UnitOfWork.SaveChanges();
                transaction.Commit();
                var azureRemoveResult = await _mediator.Send(new RemoveFromAzureInput
                {
                    Files = fileList,
                    TypeUser = Constants.Roles.AUTOESCOLA
                });
                if (!azureRemoveResult) throw new CustomException("Removido, arquivos na fila para remoção.");
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();

                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolRemoveHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }
}
