﻿using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ApiUser.GetAll

{
    public class ApiUserGetAllHandler : IRequestHandler<ApiUserGetAllInput, List<ApiUserModel>>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserGetAllHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<List<ApiUserModel>> Handle(ApiUserGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authUserRepository.Context.Set<ApiUserModel>()
                    .Include(r => r.Roles).ToListAsync();
                foreach (var item in result)
                {
                    item.RefreshToken = default;
                    item.Password = default;
                }
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}