using AulaRemota.Infra.Entity.Auth;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.ApiUser.ListarTodos
{
    public class ApiUserListarTodosInput : IRequest<List<ApiUserModel>>
    {
    }
}
