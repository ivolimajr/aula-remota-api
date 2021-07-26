using AulaRemota.Core.Entity.Auto_Escola;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class EdrivingModel
    {
        public EdrivingModel()
        {
            this.Telefones = new List<TelefoneModel>();
        }
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        public int TelefoneId { get; set; }
        public virtual ICollection<TelefoneModel> Telefones { get; set; }

        public int CargoId { get; set; }
        public EdrivingCargoModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
