using MediatR;
using Microsoft.WindowsAzure.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Arquivo.Download
{
    public class ArquivoDownloadHandler : IRequestHandler<ArquivoDownloadInput, string>
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");
        private readonly string Container = "pdfs";

        public async Task<string> Handle(ArquivoDownloadInput request, CancellationToken cancellationToken)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(Container);

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(request.NomeArquivo);

            var uri = cloudBlockBlob.Uri.AbsoluteUri;
            return uri;
        }
    }
}
