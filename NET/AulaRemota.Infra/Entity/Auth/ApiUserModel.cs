using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.Auth
{
    public class ApiUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
