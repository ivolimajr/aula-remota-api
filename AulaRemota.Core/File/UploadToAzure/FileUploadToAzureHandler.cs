using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Infra.Entity;
using System.Net;

namespace AulaRemota.Core.File.UploadToAzure
{
    public class FileUploadToAzureHandler : IRequestHandler<FileUploadToAzureInput, FileUploadToAzureResponse>
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");

        public async Task<FileUploadToAzureResponse> Handle(FileUploadToAzureInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Files != null && request.Files.Count > 0)
                {
                    var listaArquivos = new List<FileModel>();

                    foreach (var item in request.Files)
                    {
                        var fileType = Path.GetExtension(item.FileName);
                        Check.Equals(fileType.ToLower(), "pdf", "Formato não suportado");
                        Check.Equals(fileType.ToLower(), "jpg", "Formato não suportado");
                        Check.Equals(fileType.ToLower(), "jpeg", "Formato não suportado");

                        var fileResult = await SalvarNoAzure(item, request.TypeUser);
                        fileResult.Extension = fileType;
                        listaArquivos.Add(fileResult);
                    }
                    return new FileUploadToAzureResponse() { Files = listaArquivos };
                }
                return null;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(FileUploadToAzureInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        private async Task<FileModel> SalvarNoAzure(IFormFile arquivo, string typeUser)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            //Separa o upload por container no AzureBlob
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");
            if (typeUser.Equals(Constants.Roles.AUTOESCOLA)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.AUTOESCOLA).ToLower());
            if (typeUser.Equals(Constants.Roles.ADMINISTRATIVO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ADMINISTRATIVO).ToLower());
            if (typeUser.Equals(Constants.Roles.ALUNO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ALUNO).ToLower());
            if (typeUser.Equals(Constants.Roles.EDRIVING)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.EDRIVING).ToLower());
            if (typeUser.Equals(Constants.Roles.INSTRUTOR)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.INSTRUTOR).ToLower());
            if (typeUser.Equals(Constants.Roles.PARCEIRO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.PARCEIRO).ToLower());

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Off
                });
            }
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Guid.NewGuid().ToString("N").Substring(0, 5) + arquivo.FileName.Replace(" ", "").ToLower());
            cloudBlockBlob.Properties.ContentType = arquivo.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(arquivo.OpenReadStream());

            var fileResult = new FileModel()
            {
                FileName = cloudBlockBlob.Name.ToLower(),
                Extension = "",
                Destiny = cloudBlockBlob.Uri.ToString()
            };
            return fileResult;
        }
    }
}
