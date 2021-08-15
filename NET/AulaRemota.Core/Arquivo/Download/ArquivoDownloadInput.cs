using MediatR;

namespace AulaRemota.Core.Arquivo.Download
{
    public class ArquivoDownloadInput : IRequest<string>
    {
        public string NomeArquivo { get; set; }
    }
}
