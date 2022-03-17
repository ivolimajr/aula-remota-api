using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.File.DownloadFromAzure
{
    public class DownloadFileFromAzureInput : IRequest<string>
    {
        [Required]
        public string FileName { get; set; }
        public string TypeUser { get; set; }
    }
}
