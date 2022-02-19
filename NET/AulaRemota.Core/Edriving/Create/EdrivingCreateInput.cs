using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AulaRemota.Infra.Entity;

namespace AulaRemota.Core.Edriving.Create
{
    public class EdrivingCreateInput : IRequest<EdrivingCreateResponse>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        //[CpfValidador(ErrorMessage = "Cpf é Inválido")]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        public List<PhoneModel> Telefones { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Senha { get; set; }

        [Required]
        [Range(1, 100)]
        public int CargoId { get; set; }
    }
}
