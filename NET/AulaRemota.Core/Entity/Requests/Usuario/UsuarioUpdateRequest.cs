using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class UsuarioUpdateRequest
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Password { get; set; }
    }
}
