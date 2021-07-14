using AulaRemota.Core.Configuration;
using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Auth.GenerateToken
{
    class GenerateTokenHandler : IRequestHandler<GenerateTokenInput, GenerateTokenResponse>
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private readonly IRepository<AuthUserModel> _authUserRepository;

        public GenerateTokenHandler(IRepository<AuthUserModel> authUserRepository, TokenConfiguration configuration )
        {
            _authUserRepository = authUserRepository;
            _configuration = configuration;
        }

        public async Task<GenerateTokenResponse> Handle(GenerateTokenInput request, CancellationToken cancellationToken)
        {
            if(request == null) throw new HttpClientCustomException("Dados Inválidos");

            var userDb = ValidateCredentials(request);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("n")),
                new Claim(JwtRegisteredClaimNames.UniqueName, userDb.UserName)
            };

            var accessToken = GenerateAccessToken(claims);
            var refreshToken = GenerateRefreshToken();

            userDb.RefreshToken = refreshToken;
            userDb.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            try
            {
                await _authUserRepository.UpdateAsync(userDb);

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

                return new GenerateTokenResponse(
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private AuthUserModel ValidateCredentials(GenerateTokenInput user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            var userDb = _authUserRepository.Find(u => u.UserName == user.UserName);
            if (userDb == null) throw new HttpClientCustomException("Usuário Não Encontrado");

            bool checkPass = BCrypt.Net.BCrypt.Verify(user.Password, userDb.Password);
            if (!checkPass) throw new HttpClientCustomException("Credenciais Inválidas");

            return userDb;
        }
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var options = new JwtSecurityToken(issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                    signingCredentials: signinCredentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);

            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
