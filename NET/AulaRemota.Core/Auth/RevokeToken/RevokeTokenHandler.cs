﻿using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Auth.RevokeToken
{
    class RevokeTokenHandler : IRequestHandler<RevokeTokenInput, string>
    {
        private readonly IRepository<AuthUserModel> _authUserRepository;

        public RevokeTokenHandler(IRepository<AuthUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<string> Handle(RevokeTokenInput request, CancellationToken cancellationToken)
        {
            if (request.UserName == string.Empty) throw new HttpClientCustomException("Parâmetros Inválidos");

            AuthUserModel authUser = _authUserRepository.Find(u => u.UserName == request.UserName);
            if (authUser == null) throw new HttpClientCustomException("Credenciais Não encontrada");

            try
            {
                AuthUserModel usuario = await _authUserRepository.GetByIdAsync(authUser.Id);

                usuario.RefreshToken = null;
                await _authUserRepository.UpdateAsync(usuario);
                return "Usuário da Api Removido";

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
