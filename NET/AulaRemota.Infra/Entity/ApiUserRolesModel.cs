using AulaRemota.Infra.Entity.Auth;
using System.Collections.Generic;

namespace AulaRemota.Infra.Entity
{
    public class ApiUserRolesModel
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual List<ApiUserModel> Users {get; set;}
    }
}
