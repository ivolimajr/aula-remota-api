using AulaRemota.Infra.Entity.Auth;
using MediatR;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarPorIdInput : IRequest<ApiUserModel>
    {
        public int Id { get; set; }
    }
}
