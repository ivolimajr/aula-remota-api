﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Usuario.Criar
{
    public class UsuarioCriarInput : IRequest<UsuarioCriarResponse>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
