using AulaRemota.Core.DrivingSchool.Remove;
using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Instructor.Remove
{
    public class InstructorRemoveHandler : IRequestHandler<InstructorRemoveInput, bool>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public InstructorRemoveHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            UnitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<bool> Handle(InstructorRemoveInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            using var transaction = UnitOfWork.BeginTransaction();
            var fileList = new List<FileModel>();
            try
            {
                var instructorEntity = await UnitOfWork.Instructor.Context
                    .Set<InstructorModel>()
                    .Include(e => e.User)
                    .Include(e => e.PhonesNumbers)
                    .Include(e => e.Address)
                    .Include(e => e.Files)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                Check.NotNull(instructorEntity, "Não encontrado");
                Check.NotNull(instructorEntity.PhonesNumbers, "Problemas ao remover lista de telefones");
                Check.NotNull(instructorEntity.Files, "Problemas ao remover lista de arquivos");

                foreach (var item in instructorEntity.PhonesNumbers)
                    UnitOfWork.Phone.Delete(item);

                fileList = instructorEntity.Files.ToList();
                UnitOfWork.SaveChanges();

                foreach (var item in instructorEntity.Files)
                    UnitOfWork.File.Delete(item);
                UnitOfWork.SaveChanges();

                UnitOfWork.User.Delete(instructorEntity.User);
                UnitOfWork.Address.Delete(instructorEntity.Address);
                UnitOfWork.Instructor.Delete(instructorEntity);

                UnitOfWork.SaveChanges();
                transaction.Commit();
                var azureRemoveResult = await _mediator.Send(new RemoveFromAzureInput
                {
                    Files = fileList,
                    TypeUser = Constants.Roles.INSTRUTOR
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
