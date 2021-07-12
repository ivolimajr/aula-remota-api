using System.Collections.Generic;
using System.Security.Claims;

namespace AulaRemota.Core.Interfaces.Services.Auth
{
    public interface ITokenServices
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
    }
}
