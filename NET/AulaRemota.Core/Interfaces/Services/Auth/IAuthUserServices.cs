using AulaRemota.Core.Entity.Auth;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Services.Auth
{
    public interface IAuthUserServices
    {
        IEnumerable<AuthUser> GetAll();

        AuthUser GetById(int id);

        AuthUser Create(AuthUserRequest entity);

        AuthUser Update(AuthUserRequest entity);

        IEnumerable<AuthUser> GetWhere(Func<AuthUser, bool> queryLambda);

        void Delete(int id);
        bool ValidateEntity(AuthUserRequest entity);
    }
}