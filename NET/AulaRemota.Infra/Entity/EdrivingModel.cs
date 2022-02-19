using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;

namespace AulaRemota.Infra.Entity
{
    public class EdrivingModel
    {
        public EdrivingModel()
        {
            this.Telefones = new List<PhoneModel>();
        }
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public int CargoId { get; set; }
        public EdrivingLevelModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }
        public virtual List<PhoneModel> Telefones { get; set; }
    }
}
