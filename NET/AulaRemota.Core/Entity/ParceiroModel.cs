using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class ParceiroModel
    {
        public ParceiroModel()
        {
            this.Cargo = new ParceiroCargoModel();
            this.Endereco = new EnderecoModel();
            this.Usuario = new UsuarioModel();
        }
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Telefone { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string Cnpj { get; set; }

        public int CargoId { get; set; }
        public ParceiroCargoModel Cargo { get; set; }
        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

    }
}
