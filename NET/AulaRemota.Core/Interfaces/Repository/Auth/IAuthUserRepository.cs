using AulaRemota.Core.Entity.Auth;

namespace AulaRemota.Core.Interfaces.Repository.Auth
{
    public interface IAuthUserRepository: IRepository<AuthUserModel>
    {
        AuthUserModel ValidateCredentials(GetTokenRequest user);

        AuthUserModel ValidateCredentials(string userName);

        bool RevokeToken(string userName);

        AuthUserModel RefreshUserInfo(AuthUserModel user);
        AuthUserModel GetByUserName(string email);
    }
}
