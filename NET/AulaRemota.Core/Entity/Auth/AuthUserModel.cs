using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity.Auth
{
    public class AuthUserModel
    {
        public int Id { get; set; }


        [Column(TypeName = "varchar(150)")]
        public string UserName { get; set; }


        [Column(TypeName = "varchar(150)")]
        public string Nome { get; set; }


        [Column(TypeName = "varchar(150)")]
        public string Password { get; set; }


        [Column(TypeName = "varchar(250)")]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
