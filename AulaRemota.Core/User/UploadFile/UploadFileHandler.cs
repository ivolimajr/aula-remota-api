using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.User.UploadFile
{
    public class UploadFileHandler : IRequestHandler<UploadFileInput, List<FileModel>>
    {
        private List<FileModel> FileList;

        private readonly IRepository<UserModel, int> _userRepository;
        private readonly IRepository<DrivingSchoolModel, int> _drivingSchoolRepository;
        private readonly IRepository<FileModel, int> _fileRepository;
        private readonly IMediator _mediator;

        public UploadFileHandler(
            IRepository<UserModel, int> userRepository,
            IRepository<FileModel, int> fileRepository,
            IRepository<DrivingSchoolModel, int> drivingSchoolRepository,
            IMediator mediator
            )
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
            _drivingSchoolRepository = drivingSchoolRepository;
            _mediator = mediator;
            FileList = new List<FileModel>();
        }

        public async Task<List<FileModel>> Handle(UploadFileInput request, CancellationToken cancellationToken)
        {
            string typeUser = default;

            try
            {
                if (request.UserId == 0) throw new CustomException("Usuário inválido");
                Check.NotNull(request.Files, "Lista de arquivos vazia");

                if (_drivingSchoolRepository.Exists(e => e.UserId.Equals(request.UserId))) typeUser = Constants.Roles.AUTOESCOLA;
                else throw new CustomException("Usuário inválido");

                var fileResult = await _mediator.Send(new FileUploadToAzureInput
                {
                    Files = request.Files,
                    TypeUser = typeUser
                }, cancellationToken);
                Check.NotNull(fileResult, "Lista de arquivos ausente");

                //Salva no banco todas as informações dos Files do upload
                if(typeUser == Constants.Roles.AUTOESCOLA)
                    await AddFileToDrivingSchool(fileResult, request.UserId);

                Check.NotNull(FileList, "Falha ao fazer upload");

                return FileList;

            }
            catch (Exception e)
            {
                object result = new
                {
                    typeUser = typeUser ?? default,
                    userId = request.UserId
                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UploadFileHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = result
                });
            }
        }

        private async Task<bool> AddFileToDrivingSchool(FileUploadToAzureResponse azureFileList, int userId)
        {
            var drivingSchool = await _drivingSchoolRepository.Where(e => e.UserId.Equals(userId))
                .Include(e => e.Files)
                .FirstOrDefaultAsync();

            Check.NotNull(drivingSchool, "Auto Escola não encontrada");

            foreach (var item in azureFileList.Files)
            {
                var file = _fileRepository.Add(item);
                FileList.Add(file);
            }

            foreach (var item in FileList)
                drivingSchool.Files.Add(item);

            _drivingSchoolRepository.Update(drivingSchool);
            await _drivingSchoolRepository.SaveChangesAsync();

            return true;
        }
    }
}
