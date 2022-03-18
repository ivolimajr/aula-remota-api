using AulaRemota.Infra.Entity.DrivingSchool;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class FileModel
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string FileName { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Extension { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Destiny { get; set; }

        [JsonIgnore]
        public virtual DrivingSchoolModel DrivingSchool { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instructor { get; set; }
    }
}
