using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.User.UpdatePasswordByEmail
{
    public class UpdatePasswordByEmailInput : IRequest<bool>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string CurrentPassword { get; set; }

        [Required]
        [MaxLength(100)]
        public string NewPassword { get; set; }
    }
}
