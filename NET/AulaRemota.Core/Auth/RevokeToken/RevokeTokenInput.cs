using MediatR;

namespace AulaRemota.Core.Auth.RevokeToken
{
    public class RevokeTokenInput : IRequest<string>
    {
        public string UserName { get; set; }
    }
}
