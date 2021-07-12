using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class Login
    {

        [Column(TypeName = "varchar(150)")]
        public string Email{ get; set; }


        [Column(TypeName = "varchar(150)")]
        public string Password { get; set; }
    }
}
