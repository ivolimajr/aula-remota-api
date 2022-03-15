using AulaRemota.Infra.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.DrivingSchool.Create
{
    public class DrivingSchoolCreateInput : IRequest<DrivingSchoolCreateResponse>
    {
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string CorporateName { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string FantasyName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 12)]
        public string StateRegistration { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FoundingDate { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        public string Email { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 0)]
        public string Description { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Site { get; set; }

        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

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

        public ICollection<IFormFile> Files { get; set; }
    }
}
