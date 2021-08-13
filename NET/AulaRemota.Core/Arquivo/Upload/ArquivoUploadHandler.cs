using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Arquivo.Upload
{
    public class ArquivoUploadHandler : IRequestHandler<ArquivoUploadInput, ArquivoUploadResponse>
    {
        private readonly string _basePath;
        private readonly IRepository<ArquivoModel> _arquivoRepository;
        private readonly IHttpContextAccessor _context;
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");

        public ArquivoUploadHandler(IRepository<ArquivoModel> arquivoRepository, IHttpContextAccessor context)
        {
            _arquivoRepository = arquivoRepository;
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\AutoEscola\\";
        }

        public async Task<ArquivoUploadResponse> Handle(ArquivoUploadInput request, CancellationToken cancellationToken)
        {
            var listaArquivos = new List<ArquivoModel>();

            foreach (var item in request.Arquivos)
            {
                var fileType = Path.GetExtension(item.FileName);
                var baseUrl = _context.HttpContext.Request.Host;

                if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".jpeg")
                {
                    var docName = Path.GetFileName(item.FileName);
                    var destino = Path.Combine(_basePath, "", docName);

                    var arquivo = new ArquivoModel()
                    {
                        Nome = Guid.NewGuid().ToString("N").Substring(0,5) + docName,
                        Formato = fileType,
                        Destino = Path.Combine(baseUrl + "/api/v1/autoescola/" + docName)
                    };

                    await SalvarNoAzure(item);
                    using var stream = new FileStream(destino, FileMode.Create);
                    await item.CopyToAsync(stream);

                    listaArquivos.Add(arquivo);
                }
                else
                {
                    throw new HttpClientCustomException("Formato de arquivo inválido");
                }
            }

            return new ArquivoUploadResponse() { Arquivos = listaArquivos };
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
