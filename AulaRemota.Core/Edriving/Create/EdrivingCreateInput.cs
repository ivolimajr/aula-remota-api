using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AulaRemota.Infra.Entity;

namespace AulaRemota.Core.Edriving.Create
{
    public class EdrivingCreateInput : IRequest<EdrivingModel>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        //[CpfValidador(ErrorMessage = "Cpf é Inválido")]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        public List<PhoneModel> PhonesNumbers { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [Range(1, 100)]
        public int LevelId { get; set; }
    }
}
