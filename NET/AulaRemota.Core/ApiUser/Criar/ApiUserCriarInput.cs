﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserCriarInput : IRequest<ApiUserCriarResponse>
    {
        [Required]
        [EmailAddress]
        [StringLength(maximumLength:150, MinimumLength =5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }
    }
}