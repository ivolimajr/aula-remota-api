using AulaRemota.Infra.Entity.Auto_Escola;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Edriving.Atualizar
{
    public class EdrivingAtualizarInput : IRequest<EdrivingAtualizarResponse>
    {
        public EdrivingAtualizarInput()
        {
            this.Telefones = new List<TelefoneModel>();
        }

        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        public List<TelefoneModel> Telefones { get; set; }

        [Range(0, 100)]
        public int CargoId { get; set; }
    }
}
