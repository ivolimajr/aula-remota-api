using MediatR;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserListarPorIdInput : IRequest<AuthUserListarPorIdResponse>
    {
        public int Id { get; set; }
    }
}
