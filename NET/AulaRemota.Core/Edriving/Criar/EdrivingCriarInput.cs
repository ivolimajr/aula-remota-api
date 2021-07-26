using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Entity.Edriving.Criar
{
    public class EdrivingCriarInput : IRequest<EdrivingCriarResponse>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Senha { get; set; }

        [Range(0, 1)]
        public int Status { get; set; }

        [Required]
        [Range(1, 100)]
        public int CargoId { get; set; }
    }
}
