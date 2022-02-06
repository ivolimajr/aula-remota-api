using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.ApiAuth.GenerateToken
{
    public class GenerateTokenInput : IRequest<GenerateTokenResponse>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
