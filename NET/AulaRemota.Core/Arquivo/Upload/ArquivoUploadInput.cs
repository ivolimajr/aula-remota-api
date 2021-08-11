using MediatR;
using Microsoft.AspNetCore.Http;

namespace AulaRemota.Core.Arquivo.Upload
{
    public class ArquivoUploadInput : IRequest<ArquivoUploadResponse>
    {
        public IFormFile Arquivo { get; set; }
    }
}
