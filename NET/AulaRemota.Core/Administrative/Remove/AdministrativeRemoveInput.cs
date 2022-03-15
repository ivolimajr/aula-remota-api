using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Administrative.Remove
{
    public class AdministrativeRemoveInput : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
