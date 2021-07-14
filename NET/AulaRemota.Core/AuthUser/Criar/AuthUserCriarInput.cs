using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserCriarInput : IRequest<AuthUserCriarResponse>
    {
        [Required]
        [EmailAddress]
        [StringLength(maximumLength:150, MinimumLength =5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
