using AulaRemota.Infra.Entity.Auth;
using System.Collections.Generic;

namespace AulaRemota.Core.ApiUser.ListarTodos
{
    public class ApiUserListarTodosResponse
    {
        public List<ApiUserModel> Items { get; set; }
    }
}
