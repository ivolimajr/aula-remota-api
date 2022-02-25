using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.File.RemoveFile
{
    public class RemoveFileHandler : IRequestHandler<RemoveFileInput, bool>
    {
        private readonly IRepository<FileModel, int> _arquivoRepository;
        private readonly IMediator _mediator;

        public RemoveFileHandler(IRepository<FileModel, int> arquivoRepository, IMediator mediator)
        {
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveFileInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == 0) throw new CustomException("Parâmetro inválido");
                var fileResult = await _arquivoRepository.Context.Set<FileModel>()
                    .Include(e => e.DrivingSchool)
                    .Include(e => e.Instructor)
                    .Where(e => e.Id.Equals(request.Id))
                    .FirstOrDefaultAsync();
                string typeOfUser = default;
                if (fileResult == null) throw new CustomException("Arquivo não encontrado", HttpStatusCode.NotFound);

                if (fileResult.DrivingSchool != null) typeOfUser = Constants.Roles.AUTOESCOLA;
                if (fileResult.Instructor != null) typeOfUser = Constants.Roles.INSTRUTOR;

                bool result = await _mediator.Send(new RemoveFromAzureInput()
                {
                    TypeUser = typeOfUser,
                    Files = new List<FileModel>() { fileResult }
                });
                if (result)
                {
                    _arquivoRepository.Delete(fileResult);
                    _arquivoRepository.SaveChanges();
                }
                return result;                
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RemoveFileHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
