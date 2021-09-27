using MediatR;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Arquivo.Deletar
{
    class ArquivoDeletarHandler : IRequestHandler<ArquivoDeletarInput,bool>
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");
        async Task<bool> IRequestHandler<ArquivoDeletarInput, bool>.Handle(ArquivoDeletarInput request, CancellationToken cancellationToken)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");
            if (request.NivelAcesso >= 20 && request.NivelAcesso < 30) cloudBlobContainer = cloudBlobClient.GetContainerReference("autoescola");

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Off
                });
            }
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(request.NomeArquivo);

            var result = await blob.DeleteIfExistsAsync();

            return result;
        }
    }
}
