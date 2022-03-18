using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity
{
    public class PartnnerModel
    {
        public PartnnerModel()
        {
            this.PhonesNumbers = new List<PhoneModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(14)")]
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
