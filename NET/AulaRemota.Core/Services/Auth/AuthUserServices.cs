using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Interfaces.Repository.Auth;
using AulaRemota.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AulaRemota.Core.Services.Auth
{
    public class AuthUserServices : IAuthUserServices
    {

        private readonly IAuthUserRepository _authUserRepository;

        public AuthUserServices(IAuthUserRepository authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public bool ValidateEntity(AuthUserRequest entity)
        {
            if (entity.FullName == null ||
                entity.UserName == null ||
                entity.Password == null)
                return false;

            return true;
        }

        AuthUser IAuthUserServices.Create(AuthUserRequest entity)
        {
            var userExists = _authUserRepository.GetByUserName(entity.UserName);

            if (userExists != null) return null;

            entity.FullName = entity.FullName.ToUpper();
            entity.UserName = entity.UserName.ToUpper();
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

            var AuthUser = new AuthUser();
            AuthUser.FullName = entity.FullName;
            AuthUser.UserName = entity.UserName;
            AuthUser.Password = entity.Password;

            return _authUserRepository.Create(AuthUser);
        }

        bool IAuthUserServices.Delete(int id)
        {
            var result = _authUserRepository.GetById(id);
            return _authUserRepository.Delete(result);
        }

        IEnumerable<AuthUser> IAuthUserServices.GetAll()
        {
            return _authUserRepository.GetAll();
        }

        AuthUser IAuthUserServices.GetById(int id)
        {
            return _authUserRepository.GetById(id);
        }

        IEnumerable<AuthUser> IAuthUserServices.GetWhere(Expression<Func<AuthUser, bool>> predicado)
        {
            return _authUserRepository.GetWhere(predicado);
        }

        AuthUser IAuthUserServices.Update(AuthUserRequest entity)
        {
            entity.FullName = entity.FullName.ToUpper();
            entity.UserName = entity.UserName.ToUpper();

            var AuthUser = new AuthUser();
            AuthUser.FullName = entity.FullName;
            AuthUser.UserName = entity.UserName;
            return _authUserRepository.Update(AuthUser);
        }
    }
}
