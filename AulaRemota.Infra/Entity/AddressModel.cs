using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class AddressModel
    {
        public AddressModel()
        {
            this.Students = new List<StudentModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string Uf { get; set; }
        [Column(TypeName = "varchar(12)")]
        public string Cep { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string District { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string City { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string AddressNumber { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Complement { get; set; }


        [JsonIgnore]
        public virtual PartnnerModel Partnner { get; set; }
        [JsonIgnore]
        public virtual DrivingSchoolModel DrivingSchool { get; set; }
        [JsonIgnore]
        public virtual AdministrativeModel Administrative { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instructor { get; set; }
        [JsonIgnore]
        public virtual ICollection<StudentModel> Students { get; set; }

    }
}
