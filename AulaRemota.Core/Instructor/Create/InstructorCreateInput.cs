using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Instructor.Create
{
    public class InstructorCreateInput : IRequest<InstructorModel>
    {
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 5)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Identity { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        public string Origin { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 8)]
        public string Cep { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string FullAddress { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string District { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 1)]
        public string AddressNumber { get; set; }

        [MaxLength(100)]
        public string Complement { get; set; }

        [Required]
        public ICollection<PhoneModel> PhonesNumbers { get; set; }

        [Required]
        public ICollection<IFormFile> Files { get; set; }
    }
}
