using MediatR;

namespace AulaRemota.Core.ApiAuth.RefreshToken
{
    public class RefreshTokenInput : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
