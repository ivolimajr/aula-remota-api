using AulaRemota.Shared.Helpers;
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
                    var fileResult = await SalvarNoAzure(item,request.NivelAcesso);
                    fileResult.Formato = fileType;
                    listaArquivos.Add(fileResult);
                }
                else
                {
                    throw new HttpClientCustomException("Formato de arquivo inválido");
                }
            }
            return new ArquivoUploadAzureResponse() { Arquivos = listaArquivos };
        }

        private async Task<ArquivoModel> SalvarNoAzure(IFormFile arquivo, int nivelAcesso)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            
            //Separa o upload por container no AzureBlob
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");
            if (nivelAcesso >= 20 && nivelAcesso < 30) cloudBlobContainer = cloudBlobClient.GetContainerReference("autoescola");

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Off
                });
            }
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Guid.NewGuid().ToString("N").Substring(0, 5) + arquivo.FileName);
            cloudBlockBlob.Properties.ContentType = arquivo.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(arquivo.OpenReadStream());

            var fileResult = new ArquivoModel()
            {
                Nome = cloudBlockBlob.Name,
                Formato = "",
                Destino = cloudBlockBlob.Uri.ToString()
            };
            return fileResult;
        }
    }
}
