using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.File.DownloadFromAzure
{
    public class DownloadFileFromAzureHandler : IRequestHandler<DownloadFileFromAzureInput, string>
    {
        readonly CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");

        private readonly IRepository<FileModel, int> _fileRepository;

        public DownloadFileFromAzureHandler(IRepository<FileModel, int> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string> Handle(DownloadFileFromAzureInput request, CancellationToken cancellationToken)
        {
            try
            {
                var res = Exists(request.FileName.ToLower());
                if (!res) throw new CustomException("Arquivo não encontrado", HttpStatusCode.NotFound);

                string Container = request.TypeUser.ToLower();
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference(Container);
                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(request.FileName);
                var uri = cloudBlockBlob.Uri.AbsoluteUri;
                return uri;
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DownloadFileFromAzureInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }

        private bool Exists(string fileName) =>
            _fileRepository.Exists(e => e.FileName.ToLower().Equals(fileName.ToLower()));
    }
}
