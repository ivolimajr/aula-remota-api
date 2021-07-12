using AulaRemota.Core.Entity.Auth;

namespace AulaRemota.Core.Interfaces.Repository.Auth
{
    public interface IAuthUserRepository: IRepository<AuthUser>
    {
        AuthUser ValidateCredentials(GetTokenRequest user);

        AuthUser ValidateCredentials(string userName);

        bool RevokeToken(string userName);

        AuthUser RefreshUserInfo(AuthUser user);
        AuthUser GetByUserName(string email);
    }
}
