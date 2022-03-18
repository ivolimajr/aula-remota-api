using AulaRemota.Infra.Entity;
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
            this.PhonesNumbers = new List<PhoneModel>();
        }

        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public AddressModel Address { get; set; }

        public ICollection<PhoneModel> PhonesNumbers { get; set; }

        [Range(0, 100)]
        public int LevelId { get; set; }
    }
}
