﻿using AulaRemota.Shared.Configuration;
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
using System.Net;

namespace AulaRemota.Core.ApiAuth.RefreshToken
{
    class RefreshTokenHandler : IRequestHandler<RefreshTokenInput, RefreshTokenResponse>
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public RefreshTokenHandler(IRepository<ApiUserModel, int> authUserRepository, TokenConfiguration configuration)
        {
            _authUserRepository = authUserRepository;
            _configuration = configuration;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenInput request, CancellationToken cancellationToken)
        {
            try
            {
                var accessToken = request.AccessToken;
                var refreshToken = request.RefreshToken;

                var principal = GetClaimsPrincipalFromExpiredToken(accessToken);
                var userName = principal.Identity.Name;

                var user = _authUserRepository.FirstOrDefault(u => (u.UserName == userName));

                Check.NotNull(user, "Credenciais Não Encontrada");
                Check.Equals(user.RefreshToken, refreshToken, "Credenciais de Serviço Inválida");
                if (user.RefreshTokenExpiryTime < DateTime.Now) throw new CustomException("Credenciais de Serviço Expirada");

                accessToken = GenerateAccessToken(principal.Claims);
                refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                _authUserRepository.Update(user);
                
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

                await _authUserRepository.SaveChangesAsync();

                return new RefreshTokenResponse(
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                );
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RefreshTokenResponse),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.Unauthorized
                });
            }
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture)) // SE FOR NULO E SE FOR IGUAL AO HEADER ELE IRÁ LANCAR UMA EXCESSÃO
                throw new SecurityTokenException("Invalid Token");
            return principal;
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
    }
}
