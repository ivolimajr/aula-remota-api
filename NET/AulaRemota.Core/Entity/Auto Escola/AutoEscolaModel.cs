using AulaRemota.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity.Auto_Escola
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
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoEstadual { get; set; }

        public DateTime DataFundacao { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string Site { get; set; }
        public string Cnpj { get; set; }

        public int CargoId { get; set; }
        public AutoEscolaCargoModel Cargo { get; set; } 

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public virtual List<AdministrativoModel> Administrativos { get; set; }
        public virtual List<InstrutorModel> Instrutores { get; set; }
        public virtual List<CursoModel> Cursos { get; set; }
        public virtual List<TurmaModel> Turmas { get; set; }
        public virtual List<AlunoModel> Alunos { get; set; }
        public virtual List<TelefoneModel> Telefones { get; set; }
        public virtual List<ArquivoModel> Arquivos { get; set; }
    }
}
