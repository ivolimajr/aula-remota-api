using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Auth.GenerateToken
{
    public class GenerateTokenInput : IRequest<GenerateTokenResponse>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
