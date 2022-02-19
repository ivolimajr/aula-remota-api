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
            this.Alunos = new List<StudentModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string Uf { get; set; }
        [Column(TypeName = "varchar(12)")]
        public string Cep { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string EnderecoLogradouro { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Bairro { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Cidade { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Numero { get; set; }


        [JsonIgnore]
        public virtual PartnnerModel Parceiro { get; set; }
        [JsonIgnore]
        public virtual DrivingSchoolModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual AdministrativeModel Administrativo { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instrutor { get; set; }
        [JsonIgnore]
        public virtual List<StudentModel> Alunos { get; set; }

    }
}
