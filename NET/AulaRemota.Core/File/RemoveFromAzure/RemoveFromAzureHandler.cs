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
                if (request.TypeUser == null) throw new CustomException("Informe o tipo de usuário", HttpStatusCode.BadRequest );
                if (request.Files.Count > 0)
                {
                    var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    var cloudBlobContainer = cloudBlobClient.GetContainerReference("files");
                    if (request.TypeUser.Equals(Constants.Roles.AUTOESCOLA)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.AUTOESCOLA).ToLower());
                    if (request.TypeUser.Equals(Constants.Roles.ADMINISTRATIVO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ADMINISTRATIVO).ToLower());
                    if (request.TypeUser.Equals(Constants.Roles.ALUNO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.ALUNO).ToLower());
                    if (request.TypeUser.Equals(Constants.Roles.EDRIVING)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.EDRIVING).ToLower());
                    if (request.TypeUser.Equals(Constants.Roles.INSTRUTOR)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.INSTRUTOR).ToLower());
                    if (request.TypeUser.Equals(Constants.Roles.PARCEIRO)) cloudBlobContainer = cloudBlobClient.GetContainerReference(nameof(Constants.Roles.PARCEIRO).ToLower());

                    if (await cloudBlobContainer.CreateIfNotExistsAsync())
                    {
                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Off
                        });
                    }

                    foreach (var item in request.Files)
                    {
                        CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(item.FileName);

                        var result = await blob.DeleteIfExistsAsync();
                        if (!result)
                            throw new CustomException(item.FileName + "não pode ser removido");
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
