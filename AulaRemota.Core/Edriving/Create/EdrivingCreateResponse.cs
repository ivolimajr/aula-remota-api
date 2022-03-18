using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.Edriving.Create
{
    public class EdrivingCreateResponse
    {
        public EdrivingCreateResponse()
        {
            this.PhonesNumbers = new List<PhoneModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public int LevelId { get; set; }
        public EdrivingLevelModel Level { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public ICollection<PhoneModel> PhonesNumbers { get; set; }
    }
}
