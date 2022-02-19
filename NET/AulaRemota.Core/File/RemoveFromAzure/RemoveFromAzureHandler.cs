using MediatR;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Shared.Helpers;
using System.Net;

namespace AulaRemota.Core.File.RemoveFromAzure
{
    public class RemoveFromAzureHandler : IRequestHandler<RemoveFromAzureInput, bool>
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=aularemotablob;AccountKey=OBPERY2qeFupdEalWpLTXXlNmEWvQVKljJWkYgrr1dTOalbL/R/KxF82qEdk+4rVD2mx2CR28KqS8vkxvn5jkg==;EndpointSuffix=core.windows.net");
        async Task<bool> IRequestHandler<RemoveFromAzureInput, bool>.Handle(RemoveFromAzureInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.TipoUsuario == null) throw new CustomException(new ResponseModel() { UserMessage = "Informe o tipo de usuário", ModelName = nameof(RemoveFromAzureInput), StatusCode = HttpStatusCode.BadRequest });
                if (request.Arquivos.Count > 0)
                {
                    var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");
                    if (request.TipoUsuario.Equals(Constants.Roles.AUTOESCOLA)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.AUTOESCOLA).ToLower());
                    if (request.TipoUsuario.Equals(Constants.Roles.ADMINISTRATIVO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ADMINISTRATIVO).ToLower());
                    if (request.TipoUsuario.Equals(Constants.Roles.ALUNO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ALUNO).ToLower());
                    if (request.TipoUsuario.Equals(Constants.Roles.EDRIVING)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.EDRIVING).ToLower());
                    if (request.TipoUsuario.Equals(Constants.Roles.INSTRUTOR)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.INSTRUTOR).ToLower());
                    if (request.TipoUsuario.Equals(Constants.Roles.PARCEIRO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.PARCEIRO).ToLower());

                    if (await cloudBlobContainer.CreateIfNotExistsAsync())
                    {
                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Off
                        });
                    }

                    foreach (var item in request.Arquivos)
                    {
                        CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(item.Nome);

                        var result = await blob.DeleteIfExistsAsync();
                        if (!result)
                            throw new CustomException(item.Nome + "não pode ser removido");
                    }
                }
                return true;

            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RemoveFromAzureHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
