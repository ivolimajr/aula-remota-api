using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.File.RemoveFile
{
    public class RemoveFileInput : IRequest<bool>
    {
        [Required]
        [Range(1,9999)]
        public int Id { get; set; }
    }
}
