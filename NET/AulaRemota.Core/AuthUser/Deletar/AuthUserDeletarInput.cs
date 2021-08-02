using MediatR;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserDeletarInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
