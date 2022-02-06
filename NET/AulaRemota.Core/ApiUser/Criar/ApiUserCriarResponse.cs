using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserCriarResponse
    {
        public ApiUserCriarResponse()
        {
            Roles = new List<RolesModel>();
        }
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public List<RolesModel> Roles { get; set; }
    }
}
