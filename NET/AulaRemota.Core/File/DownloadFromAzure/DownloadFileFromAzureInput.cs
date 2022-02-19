using MediatR;

namespace AulaRemota.Core.File.DownloadFromAzure
{
    public class DownloadFileFromAzureInput : IRequest<string>
    {
        public string NomeArquivo { get; set; }
    }
}
