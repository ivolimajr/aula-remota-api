using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.User.UpdatePassword
{
    public class UpdatePasswordInput : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CurrentPassword { get; set; }

        [Required]
        [MaxLength(100)]
        public string NewPassword { get; set; }
    }
}
