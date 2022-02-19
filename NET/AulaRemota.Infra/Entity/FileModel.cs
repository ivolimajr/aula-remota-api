using AulaRemota.Infra.Entity.DrivingSchool;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Formato { get; set; }
        public string Destino { get; set; }

        [JsonIgnore]
        public virtual DrivingSchoolModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instrutor { get; set; }
    }
}
