using AulaRemota.Infra.Entity.Auth;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarTodosInput : IRequest<List<ApiUserModel>>
    {
    }
}
