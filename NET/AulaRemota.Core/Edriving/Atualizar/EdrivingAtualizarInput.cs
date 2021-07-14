using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Edriving.Atualizar
{
    public class EdrivingAtualizarInput : IRequest<EdrivingAtualizarResponse>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string FullName { get; set; }

        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Telefone { get; set; }

        [Range(0, 100)]
        public int CargoId { get; set; }
    }
}
