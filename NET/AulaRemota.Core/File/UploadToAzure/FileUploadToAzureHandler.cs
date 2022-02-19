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
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Infra.Entity;

namespace AulaRemota.Core.File.UploadToAzure
{
    public class FileUploadToAzureHandler : IRequestHandler<FileUploadToAzureInput, FileUploadToAzureResponse>
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");

        public async Task<FileUploadToAzureResponse> Handle(FileUploadToAzureInput request, CancellationToken cancellationToken)
        {
            if (request.Arquivos.Count > 0)
            {
                var listaArquivos = new List<FileModel>();

                foreach (var item in request.Arquivos)
                {
                    var fileType = Path.GetExtension(item.FileName);
                    if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".jpeg")
                    {
                        var fileResult = await SalvarNoAzure(item, request.TipoUsuario);
                        fileResult.Formato = fileType;
                        listaArquivos.Add(fileResult);
                    }
                    else
                    {
                        throw new CustomException("Formato de arquivo inválido");
                    }
                }
                return new FileUploadToAzureResponse() { Arquivos = listaArquivos };
            }
            return null;
        }

        private async Task<FileModel> SalvarNoAzure(IFormFile arquivo, string tipoUsuario)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            //Separa o upload por container no AzureBlob
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");
            if (tipoUsuario.Equals(Constants.Roles.AUTOESCOLA)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.AUTOESCOLA).ToLower());
            if (tipoUsuario.Equals(Constants.Roles.ADMINISTRATIVO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ADMINISTRATIVO).ToLower());
            if (tipoUsuario.Equals(Constants.Roles.ALUNO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ALUNO).ToLower());
            if (tipoUsuario.Equals(Constants.Roles.EDRIVING)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.EDRIVING).ToLower());
            if (tipoUsuario.Equals(Constants.Roles.INSTRUTOR)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.INSTRUTOR).ToLower());
            if (tipoUsuario.Equals(Constants.Roles.PARCEIRO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.PARCEIRO).ToLower());

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Off
                });
            }
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Guid.NewGuid().ToString("N").Substring(0, 5) + arquivo.FileName.Replace(" ", ""));
            cloudBlockBlob.Properties.ContentType = arquivo.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(arquivo.OpenReadStream());

            var fileResult = new FileModel()
            {
                Nome = cloudBlockBlob.Name,
                Formato = "",
                Destino = cloudBlockBlob.Uri.ToString()
            };
            return fileResult;
        }
    }
}
