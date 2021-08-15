using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.UploadLocal
{
    public class ArquivoUploadLocalInput : IRequest<ArquivoUploadLocalResponse>
    {
        
        public List<IFormFile> Arquivos { get; set; }
        public string NomeAutoEscola { get; set; }
    }
}
