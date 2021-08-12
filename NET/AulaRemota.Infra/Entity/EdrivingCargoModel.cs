using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class EdrivingCargoModel
    {
        public EdrivingCargoModel()
        {
            this.Edrivings = new List<EdrivingModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        [MinLength(3)]
        public string Cargo { get; set; }

        [JsonIgnore]
        public virtual List<EdrivingModel> Edrivings { get; set; }
    }
}
