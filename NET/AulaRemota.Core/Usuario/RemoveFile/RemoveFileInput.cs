using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Usuario.RemoveFile
{
    public class RemoveFileInput : IRequest<bool>
    {
        [Required]
        [Range(1,9999)]
        public int IdArquivo { get; set; }
    }
}
