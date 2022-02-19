using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity
{
    public class PartnnerModel
    {
        public PartnnerModel()
        {
            this.Telefones = new List<PhoneModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string Cnpj { get; set; }

        public int CargoId { get; set; }
        public PartnnerLevelModel Cargo { get; set; }
        public int EnderecoId { get; set; }
        public AddressModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }
        public virtual List<PhoneModel> Telefones { get; set; }

    }
}
