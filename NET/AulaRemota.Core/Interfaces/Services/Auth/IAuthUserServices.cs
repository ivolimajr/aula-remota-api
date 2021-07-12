using AulaRemota.Core.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Services.Auth
{
    public interface IAuthUserServices
    {
        IEnumerable<AuthUser> GetAll();

        AuthUser GetById(int id);

        AuthUser Create(AuthUserRequest entity);

        AuthUser Update(AuthUserRequest entity);

        IEnumerable<AuthUser> GetWhere(Expression<Func<AuthUser, bool>> predicado);

        bool Delete(int id);
        bool ValidateEntity(AuthUserRequest entity);
    }
}