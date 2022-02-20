using AulaRemota.Infra.Entity.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class RolesModel
    {
        public RolesModel()
        {
            ApiUsers = new List<ApiUserModel>();
            Users = new List<UserModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApiUserModel> ApiUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
