using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Administrative.Create
{
    public class AdministrativeCreateInput : IRequest<AdministrativeModel>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(70)]
        public string Email { get; set; }

        [Required]
        [MaxLength(14)]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(20)]
        public string Identity { get; set; }

        [Required]
        [MaxLength(70)]
        public string Origin { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public AddressModel Address { get; set; }

        [Required]
        [Range(1,9999)]
        public int DrivingSchoolId { get; set; }

        [Required]
        public ICollection<PhoneModel> PhonesNumbers { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
