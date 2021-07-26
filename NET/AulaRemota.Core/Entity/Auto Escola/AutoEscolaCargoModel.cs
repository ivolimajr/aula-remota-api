using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class AutoEscolaCargoModel
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Cargo { get; set; }
    }
}
