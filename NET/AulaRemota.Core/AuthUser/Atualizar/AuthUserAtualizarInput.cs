using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.AuthUser.Atualizar
{
    public class AuthUserAtualizarInput : IRequest<AuthUserAtualizarResponse>
    {
        [Required]
        [Range(1,99999)]
        public int Id { get; set; }

        [EmailAddress]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string UserName { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Nome { get; set; }
    }
}
