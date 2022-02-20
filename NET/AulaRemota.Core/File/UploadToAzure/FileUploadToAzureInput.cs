using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.File.UploadToAzure
{
    public class FileUploadToAzureInput : IRequest<FileUploadToAzureResponse>
    {   
        public ICollection<IFormFile> Files { get; set; }
        public string TypeUser { get; set; }
    }
}
