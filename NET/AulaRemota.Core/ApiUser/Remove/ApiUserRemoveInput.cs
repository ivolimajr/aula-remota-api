using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.ApiUser.Remove
{
    public class ApiUserRemoveInput : IRequest<bool>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }
    }
}
