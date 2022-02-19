using AulaRemota.Infra.Models;
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
            this.Arquivos = new List<FileModel>();
            this.Telefones = new List<PhoneModel>();
            this.Turmas = new List<TurmaModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string RazaoSocial { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string NomeFantasia { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string InscricaoEstadual { get; set; }

        public DateTime DataFundacao { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Site { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string Cnpj { get; set; }

        public int EnderecoId { get; set; }
        public AddressModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }

        [JsonIgnore]
        public virtual List<AdministrativeModel> Administrativos { get; set; }
        [JsonIgnore]
        public virtual List<InstructorModel> Instrutores { get; set; }
        [JsonIgnore]
        public virtual List<CourseModel> Cursos { get; set; }
        [JsonIgnore]
        public virtual List<TurmaModel> Turmas { get; set; }
        [JsonIgnore]
        public virtual List<StudentModel> Alunos { get; set; }

        public virtual List<PhoneModel> Telefones { get; set; }

        public virtual List<FileModel> Arquivos { get; set; }
    }
}
