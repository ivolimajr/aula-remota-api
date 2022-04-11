using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Instructor.Update
{
    public class InstructorUpdateInput : IRequest<InstructorModel>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(70)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(14)]
        public string Cpf { get; set; }

        [MaxLength(20)]
        public string Identity { get; set; }

        [MaxLength(70)]
        public string Origin { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [MaxLength(150)]
        public string Password { get; set; }

        [MaxLength(2)]
        public string Uf { get; set; }

        [MaxLength(8)]
        public string Cep { get; set; }

        [MaxLength(100)]
        public string FullAddress { get; set; }

        [MaxLength(80)]
        public string District { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(10)]
        public string AddressNumber { get; set; }

        [MaxLength(100)]
        public string Complement { get; set; }

        public ICollection<PhoneModel> PhonesNumbers { get; set; }

        public ICollection<IFormFile> Files { get; set; }
    }
}
