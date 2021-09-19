using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.UploadAzure
{
    public class ArquivoUploadAzureInput : IRequest<ArquivoUploadAzureResponse>
    {   
        public List<IFormFile> Arquivos { get; set; }
        public int NivelAcesso { get; set; }
    }
}
