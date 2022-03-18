using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class StudentModel
    {

        public StudentModel()
        {
            this.PhonesNumbers = new List<PhoneModel>();
        }

        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Identity { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Origin { get; set; }
        public DateTime Birthdate { get; set; }

        public int ClassId { get; set; }
        public TurmaModel Class { get; set; }

        public int DrivingSchoolId { get; set; }
        public DrivingSchoolModel DrivingSchool { get; set; }

        public int AddressId { get; set; }
        public AddressModel Address { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public ICollection<PhoneModel> PhonesNumbers { get; set; }
    }
}