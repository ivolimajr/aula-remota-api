using AulaRemota.Shared.Helpers.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.UploadAzure
{
    public class ArquivoUploadAzureInput : IRequest<ArquivoUploadAzureResponse>
    {   
        public List<IFormFile> Arquivos { get; set; }
        public string TipoUsuario { get; set; }
    }
}
