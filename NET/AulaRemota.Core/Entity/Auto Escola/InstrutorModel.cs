using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class InstrutorModel
    {
        public InstrutorModel()
        {
            this.Cargo = new AutoEscolaCargoModel();
            this.Endereco = new EnderecoModel();
            this.Usuario = new UsuarioModel();
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

        [Column(TypeName = "varchar(15)")]
        public string Telefone { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string TelefoneFixo { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int CargoId { get; set; }
        public AutoEscolaCargoModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}