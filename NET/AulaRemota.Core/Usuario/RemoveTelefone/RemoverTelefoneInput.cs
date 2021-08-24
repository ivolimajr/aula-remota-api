using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Usuario.RemoveTelefone
{
    public class RemoveTelefoneInput : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
