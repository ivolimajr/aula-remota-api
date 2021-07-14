using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Interfaces.Repository.Auth;
using AulaRemota.Infra.Data;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AulaRemota.Infra.Repository.Auth
{
    public class AuthUserRepository : Repository<AuthUserModel>, IAuthUserRepository
    {
        public AuthUserRepository(MySqlContext context) : base(context)
        {

        }

        AuthUserModel IAuthUserRepository.RefreshUserInfo(AuthUserModel user)
        {
            if (!_context.AuthUser.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.AuthUser.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        bool IAuthUserRepository.RevokeToken(string userName)
        {
            var user = _context.AuthUser.SingleOrDefault(u => (u.UserName == userName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        AuthUserModel IAuthUserRepository.ValidateCredentials(GetTokenRequest user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            var result = _context.AuthUser.FirstOrDefault(u => u.UserName == user.UserName);
            bool checkPass = BCrypt.Net.BCrypt.Verify(user.Password, result.Password);
            if (!checkPass) return null;
            return result;
        }

        AuthUserModel IAuthUserRepository.ValidateCredentials(string userName)
        {
            return _context.AuthUser.SingleOrDefault(u => (u.UserName == userName));
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public AuthUserModel GetByUserName(string userName)
        {
            try
            {
                return _context.Set<AuthUserModel>().Where(u => u.UserName == userName).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
