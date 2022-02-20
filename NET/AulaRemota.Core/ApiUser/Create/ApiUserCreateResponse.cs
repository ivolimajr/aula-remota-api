using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.ApiUser.Create
{
    public class ApiUserCreateResponse
    {
        public ApiUserCreateResponse()
        {
            Roles = new List<RolesModel>();
        }
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public ICollection<RolesModel> Roles { get; set; }
    }
}
