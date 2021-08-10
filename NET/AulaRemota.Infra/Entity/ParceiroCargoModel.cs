﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class ParceiroCargoModel
    {
        public ParceiroCargoModel()
        {
            this.Parceiros = new List<ParceiroModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Cargo { get; set; }

        [JsonIgnore]
        public virtual List<ParceiroModel> Parceiros { get; set; }
    }
}