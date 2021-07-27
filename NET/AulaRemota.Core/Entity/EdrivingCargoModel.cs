using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Entity
{
    public class EdrivingCargoModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Cargo { get; set; }

        public virtual List<EdrivingModel> Edriving { get; set; }
    }
}
