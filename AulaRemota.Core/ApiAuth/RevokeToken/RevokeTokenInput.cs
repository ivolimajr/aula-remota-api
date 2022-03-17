using MediatR;

namespace AulaRemota.Core.ApiAuth.RevokeToken
{
    public class RevokeTokenInput : IRequest<string>
    {
        public string UserName { get; set; }
    }
}
