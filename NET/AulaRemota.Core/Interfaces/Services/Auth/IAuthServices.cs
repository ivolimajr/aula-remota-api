using AulaRemota.Core.Entity.Auth;

namespace AulaRemota.Core.Interfaces.Services.Auth
{
    public interface IAuthServices
    {
        Token ValidateCredentials(GetTokenRequest user);
        Token ValidateCredentials(Token token);
        bool RevokeToken(string userName);
    }
}
