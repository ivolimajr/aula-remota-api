using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.File.UploadToAzure
{
    public class FileUploadToAzureInput : IRequest<FileUploadToAzureResponse>
    {   
        public List<IFormFile> Arquivos { get; set; }
        public string TipoUsuario { get; set; }
    }
}
