using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.User.Login
{
    public class UserLoginInput : IRequest<UserLoginResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
