using AulaRemota.Infra.Entity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AulaRemota.Core.User.Login
{
    public class UserLoginResponse
    {
        public UserLoginResponse()
        {
            Roles = new List<RolesModel>();
            Address = new AddressModel();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public AddressModel Address { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public int Status { get; set; } // 0 -> DELETADO (DELETE) | 1 -> ATIVO | 2 ->INATIVADO
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public virtual ICollection<RolesModel> Roles { get; set; }

    }
}
