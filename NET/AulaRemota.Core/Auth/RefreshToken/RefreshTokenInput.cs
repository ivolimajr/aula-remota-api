using MediatR;

namespace AulaRemota.Core.Auth.RefreshToken
{
    public class RefreshTokenInput : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
