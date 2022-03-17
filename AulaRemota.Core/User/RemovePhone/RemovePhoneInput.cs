using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.User.RemovePhone
{
    public class RemovePhoneInput : IRequest<bool>
    {
        [Required]
        [Range(1,9999)]
        public int Id { get; set; }
    }
}
