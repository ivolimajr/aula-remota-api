using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Administrative.Update
{
    public class AdministrativeUpdateInput : IRequest<AdministrativeModel>
    {
        [Required]
        [Range(1,99999)]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(70)]
        public string Email { get; set; }

        [MaxLength(14)]
        public string Cpf { get; set; }

        [MaxLength(20)]
        public string Identity { get; set; }

        [MaxLength(70)]
        public string Origin { get; set; }

        public DateTime Birthdate { get; set; }

        public AddressModel Address { get; set; }

        public ICollection<PhoneModel> PhonesNumbers { get; set; }
    }
}
