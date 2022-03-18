using AulaRemota.Infra.Entity.Auth;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.ApiUser.GetAll

{
    public class ApiUserGetAllHandler : IRequestHandler<ApiUserGetAllInput, List<ApiUserModel>>
    {
        private readonly IRepository<ApiUserModel, int> _authUserRepository;

        public ApiUserGetAllHandler(IRepository<ApiUserModel, int> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<List<ApiUserModel>> Handle(ApiUserGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                var newsDtoList = await _authUserRepository.Context
                    .Set<ApiUserModel>()
                    .Select(x => new ApiUserModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Roles = x.Roles,
                        UserName = x.UserName,
                        RefreshTokenExpiryTime = x.RefreshTokenExpiryTime,
                    })
                    .ToListAsync();
                return newsDtoList;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(ApiUserGetAllHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NoContent
                });
            }
        }
    }
}
