using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class Parceiro
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Telefone { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string Cpnpj { get; set; }

        public int CargoId { get; set; }
        public ParceiroCargo Cargo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
