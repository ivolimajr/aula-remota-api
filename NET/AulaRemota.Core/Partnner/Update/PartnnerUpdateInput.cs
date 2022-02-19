using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Partnner.Update
{
    public class PartnnerUpdateInput : IRequest<PartnnerUpdateResponse>
    {
        public PartnnerUpdateInput()
        {
            this.Telefones = new List<PhoneModel>();
        }

        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Descricao { get; set; }

        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [StringLength(maximumLength: 8, MinimumLength = 8)]
        public string Cep { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string EnderecoLogradouro { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Bairro { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Cidade { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Numero { get; set; }

        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        public List<PhoneModel> Telefones { get; set; }

        [Range(0, 100)]
        public int CargoId { get; set; }
    }
}
