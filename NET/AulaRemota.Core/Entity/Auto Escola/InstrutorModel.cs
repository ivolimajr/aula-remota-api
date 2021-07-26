using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class InstrutorModel
    {
        public InstrutorModel()
        {
            this.Cargo = new AutoEscolaCargoModel();
            this.Endereco = new EnderecoModel();
            this.Usuario = new UsuarioModel();
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
        public string Orgão { get; set; }
        public DateTime Aniversario { get; set; }
        public bool Status { get; set; }

        public int TelefoneId { get; set; }
        public ICollection<TelefoneModel> Telefones { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int CargoId { get; set; }
        public AutoEscolaCargoModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public int AutoEscolaId { get; set; }
        public virtual ICollection<AutoEscolaModel> AutoEscola { get; set; }
    }
}