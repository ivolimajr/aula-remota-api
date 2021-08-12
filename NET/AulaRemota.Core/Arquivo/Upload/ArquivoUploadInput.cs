using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.Upload
{
    public class ArquivoUploadInput : IRequest<ArquivoUploadResponse>
    {
        
        public List<IFormFile> Arquivos { get; set; }
        public string NomeAutoEscola { get; set; }
    }
}
