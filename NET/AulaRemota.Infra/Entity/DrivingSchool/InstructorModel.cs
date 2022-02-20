using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class InstructorModel
    {
        public InstructorModel()
        {
            this.PhonesNumbers = new List<PhoneModel>();
            this.Courses = new List<CourseModel>();
            this.DrivingScools = new List<DrivingSchoolModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string Cpf { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Identity { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Origin { get; set; }
        public DateTime Birthdate { get; set; }

        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }

        public virtual ICollection<DrivingSchoolModel> DrivingScools { get; set; }
        public virtual ICollection<CourseModel> Courses { get; set; }
        public virtual ICollection<PhoneModel> PhonesNumbers { get; set; }
        public virtual ICollection<FileModel> Files { get; set; }
    }
}