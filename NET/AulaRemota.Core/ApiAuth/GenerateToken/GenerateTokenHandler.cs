using AulaRemota.Shared.Configuration;
using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ApiAuth.GenerateToken
{
    class GenerateTokenHandler : IRequestHandler<GenerateTokenInput, GenerateTokenResponse>
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public GenerateTokenHandler(IRepository<ApiUserModel> authUserRepository, TokenConfiguration configuration)
        {
            _authUserRepository = authUserRepository;
            _configuration = configuration;
        }

        public async Task<GenerateTokenResponse> Handle(GenerateTokenInput request, CancellationToken cancellationToken)
        {
            if (request == null) throw new CustomException("Dados Inválidos");

            try
            {
                var user = ValidateCredentials(request);

                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("n")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

                var accessToken = GenerateAccessToken(claims);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

                _authUserRepository.Update(user);

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

                await _authUserRepository.SaveChangesAsync();

                return new GenerateTokenResponse(
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                );
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private ApiUserModel ValidateCredentials(GenerateTokenInput user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            var userDb = _authUserRepository.Find(u => u.UserName == user.UserName);
            if (userDb == null) throw new CustomException("Usuário Não Encontrado");

            bool checkPass = BCrypt.Net.BCrypt.Verify(user.Password, userDb.Password);
            if (!checkPass) throw new CustomException("Credenciais Inválidas");

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
