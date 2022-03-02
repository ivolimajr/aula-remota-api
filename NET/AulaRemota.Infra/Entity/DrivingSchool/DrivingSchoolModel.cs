using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class DrivingSchoolModel
    {
        public DrivingSchoolModel()
        {
            Files = new List<FileModel>();
            PhonesNumbers = new List<PhoneModel>();
            Administratives = new List<AdministrativeModel>();
            Classes = new List<TurmaModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string CorporateName { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string FantasyName { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string StateRegistration { get; set; }

        public DateTime FoundingDate { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Site { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string Cnpj { get; set; }

        public int AddressId { get; set; }
        public AddressModel Address { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        
        public virtual ICollection<AdministrativeModel> Administratives { get; set; }
        [JsonIgnore]
        public virtual ICollection<InstructorModel> Instructors { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseModel> Courses { get; set; }
        [JsonIgnore]
        public virtual ICollection<TurmaModel> Classes { get; set; }
        [JsonIgnore]
        public virtual ICollection<StudentModel> Sudents { get; set; }

        public virtual ICollection<PhoneModel> PhonesNumbers { get; set; }

        public virtual ICollection<FileModel> Files { get; set; }
    }
}
