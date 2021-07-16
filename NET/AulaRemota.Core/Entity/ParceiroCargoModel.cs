using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class ParceiroCargoModel
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Cargo { get; set; }
    }
}
