using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerResponse
    {
        public CreatePartnnerResponse()
        {
            this.PhonesNumbers = new List<PhoneModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Cnpj { get; set; }

        public int LevelId { get; set; }
        public PartnnerLevelModel Level { get; set; }
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public virtual ICollection<PhoneModel> PhonesNumbers { get; set; }
    }
}
