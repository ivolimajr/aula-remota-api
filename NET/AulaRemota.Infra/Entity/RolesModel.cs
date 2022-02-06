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
            Usuarios = new List<UsuarioModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; }

        [JsonIgnore]
        public virtual List<ApiUserModel> ApiUsers { get; set; }
        [JsonIgnore]
        public virtual List<UsuarioModel> Usuarios { get; set; }
    }
}
