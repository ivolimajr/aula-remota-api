using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class PartnnerLevelModel
    {
        public PartnnerLevelModel()
        {
            this.Partnners = new List<PartnnerModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        [MinLength(3)]
        public string Level { get; set; }

        [JsonIgnore]
        public virtual ICollection<PartnnerModel> Partnners { get; set; }
    }
}
