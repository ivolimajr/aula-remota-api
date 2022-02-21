﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class EdrivingLevelModel
    {
        public EdrivingLevelModel()
        {
            this.Edrivings = new List<EdrivingModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        [MinLength(3)]
        public string Level { get; set; }

        [JsonIgnore]
        public virtual ICollection<EdrivingModel> Edrivings { get; set; }
    }
}