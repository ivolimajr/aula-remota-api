using AulaRemota.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity.Auto_Escola
{
    public class AutoEscolaModel
    {
        public AutoEscolaModel()
        {
            this.Arquivos = new List<ArquivoModel>();
            this.Telefones = new List<TelefoneModel>();
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
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        [JsonIgnore]
        public virtual List<AdministrativoModel> Administrativos { get; set; }
        [JsonIgnore]
        public virtual List<InstrutorModel> Instrutores { get; set; }
        [JsonIgnore]
        public virtual List<CursoModel> Cursos { get; set; }
        [JsonIgnore]
        public virtual List<TurmaModel> Turmas { get; set; }
        [JsonIgnore]
        public virtual List<AlunoModel> Alunos { get; set; }

        public virtual List<TelefoneModel> Telefones { get; set; }

        public virtual List<ArquivoModel> Arquivos { get; set; }
    }
}
