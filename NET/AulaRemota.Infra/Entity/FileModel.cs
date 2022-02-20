using AulaRemota.Infra.Entity.DrivingSchool;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Destiny { get; set; }

        [JsonIgnore]
        public virtual DrivingSchoolModel DrivingSchool { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instructor { get; set; }
    }
}
