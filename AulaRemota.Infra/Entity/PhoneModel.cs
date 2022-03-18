using AulaRemota.Infra.Entity.DrivingSchool;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class PhoneModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 10)]
        [Column(TypeName = "varchar(14)")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public virtual EdrivingModel Edriving { get; set; }
        [JsonIgnore]
        public virtual PartnnerModel Partnner { get; set; }
        [JsonIgnore]
        public virtual DrivingSchoolModel DrivingSchool { get; set; }
        [JsonIgnore]
        public virtual AdministrativeModel Administrative { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instructor { get; set; }
        [JsonIgnore]
        public virtual StudentModel Student { get; set; }
    }
}
