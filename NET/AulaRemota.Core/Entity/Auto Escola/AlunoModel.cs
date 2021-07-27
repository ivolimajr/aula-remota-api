using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class AlunoModel
    {

        public AlunoModel()
        {
            this.Telefones = new List<TelefoneModel>();
        }

        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Identidade { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Orgao { get; set; }
        public DateTime Aniversario { get; set; }

        public int TurmaId { get; set; }
        public TurmaModel Turma { get; set; }

        public int AutoEscolaId { get; set; }
        public AutoEscolaModel AutoEscola { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public List<TelefoneModel> Telefones { get; set; }
    }
}