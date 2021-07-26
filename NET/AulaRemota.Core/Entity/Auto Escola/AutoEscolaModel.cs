using AulaRemota.Core.Entity.Auto_Escola;
using AulaRemota.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class AutoEscolaModel
    {
        public AutoEscolaModel()
        {
            this.Arquivos = new List<ArquivoModel>();
            this.Endereco = new EnderecoModel();
            this.Usuario = new UsuarioModel();
            this.Telefones = new List<TelefoneModel>();
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
        public bool Status { get; set; }

        public int TelefoneId { get; set; }
        public ICollection<TelefoneModel> Telefones { get; set; }

        public int ArquivoId { get; set; }
        public virtual ICollection<ArquivoModel> Arquivos { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public virtual ICollection<AdministrativoModel> Administrativos { get; set; }
        public virtual ICollection<InstrutorModel> Instrutores { get; set; }
        public virtual ICollection<CursoModel> Cursos { get; set; }
        public virtual ICollection<TurmaModel> Turmas { get; set; }
        public virtual ICollection<AlunoModel> Alunos { get; set; }
    }
}
