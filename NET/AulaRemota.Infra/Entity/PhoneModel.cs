using AulaRemota.Infra.Entity.DrivingSchool;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class PhoneModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 10)]
        public string Telefone { get; set; }

        [JsonIgnore]
        public virtual EdrivingModel Edriving { get; set; }
        [JsonIgnore]
        public virtual PartnnerModel Parceiro { get; set; }
        [JsonIgnore]
        public virtual DrivingSchoolModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual AdministrativeModel Administrativo { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instrutor { get; set; }
        [JsonIgnore]
        public virtual StudentModel Aluno { get; set; }
    }
}
