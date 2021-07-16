using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class EdrivingCargoModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        [Column(TypeName = "varchar(100)")]
        public string Cargo { get; set; }
    }
}
