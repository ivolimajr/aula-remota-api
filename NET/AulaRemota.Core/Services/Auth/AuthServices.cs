using AulaRemota.Core.Configuration;
using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Interfaces.Repository.Auth;
using AulaRemota.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AulaRemota.Core.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IAuthUserRepository _repository;
        private readonly ITokenServices _tokenService;

        public AuthServices(TokenConfiguration configuration, IAuthUserRepository repository, ITokenServices tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }


        Token IAuthServices.ValidateCredentials(GetTokenRequest userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials); // PEGA AS CREDENCIAIS E VALIDA NO BANCO
            if (user == null) return null; // SE NÃO RETORNAR UM USER DO BANCO DE DADOS, RETORNA NULL

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("n")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new Token(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        Token IAuthServices.ValidateCredentials(Token token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;

            var user = _repository.ValidateCredentials(userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);

            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new Token(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }
        bool IAuthServices.RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
