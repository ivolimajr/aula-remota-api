using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Models;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.DrivingSchool.Create
{
    public class DrivingSchoolCreateResponse
    {
        public DrivingSchoolCreateResponse()
        {
            this.PhonesNumbers = new List<PhoneModel>();
            this.Files = new List<FileModel>();
        }
        public int Id { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public string InscricaoEstadual { get; set; }

        public DateTime DataFundacao { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public string Cnpj { get; set; }

        public int AddressId { get; set; }
        public AddressModel Address { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
        public virtual ICollection<PhoneModel> PhonesNumbers { get; set; }
        public virtual ICollection<FileModel> Files { get; set; }
    }
}
