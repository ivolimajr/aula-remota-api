using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Arquivo.UploadAzure
{
    public class ArquivoUploadAzureHandler : IRequestHandler<ArquivoUploadAzureInput, ArquivoUploadAzureResponse>
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");

        public async Task<ArquivoUploadAzureResponse> Handle(ArquivoUploadAzureInput request, CancellationToken cancellationToken)
        {
            var listaArquivos = new List<ArquivoModel>();

            foreach (var item in request.Arquivos)
            {
                var fileType = Path.GetExtension(item.FileName);

                if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".jpeg")
                {
                    var docName = Path.GetFileName(item.FileName);

                    var arquivo = new ArquivoModel()
                    {
                        Nome = Guid.NewGuid().ToString("N").Substring(0, 5) + docName,
                        Formato = fileType,
                        Destino = ""
                    };

                    await SalvarNoAzure(item);

                    listaArquivos.Add(arquivo);
                }
                else
                {
                    throw new HttpClientCustomException("Formato de arquivo inválido");
                }
            }

            return new ArquivoUploadAzureResponse() { Arquivos = listaArquivos };
        }

        private async Task SalvarNoAzure(IFormFile arquivo)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference("pdfs");

            if(await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Off
                });
            }
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(arquivo.FileName);
            cloudBlockBlob.Properties.ContentType = arquivo.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(arquivo.OpenReadStream());
        }
    }
}
