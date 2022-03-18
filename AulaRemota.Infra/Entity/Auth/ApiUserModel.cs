using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.Auth
{
    public class ApiUserModel
    {
        public ApiUserModel()
        {
            Roles = new List<RolesModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<RolesModel> Roles { get; set; }

    }
}
