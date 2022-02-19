using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerResponse
    {
        public CreatePartnnerResponse()
        {
            this.Telefones = new List<PhoneModel>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
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
