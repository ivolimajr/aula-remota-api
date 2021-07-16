using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.AuthUser.Login
{
    public class AuthUserLoginInput : IRequest<AuthUserLoginResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
