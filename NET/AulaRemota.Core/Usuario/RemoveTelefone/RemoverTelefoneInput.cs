using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Usuario.RemoveTelefone
{
    public class RemoveTelefoneInput : IRequest<bool>
    {
        [Required]
        [Range(1,9999)]
        public int Id { get; set; }
    }
}
