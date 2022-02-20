using AulaRemota.Infra.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerInput : IRequest<CreatePartnnerResponse>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Description { get; set; }

        public List<PhoneModel> PhonesNumbers { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [Range(1, 100)]
        public int LevelId { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 8)]
        public string Cep { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string District { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Number { get; set; }
    }
}
