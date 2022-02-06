using AulaRemota.Infra.Entity.Auth;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.ApiUser.GetAll
{
    public class ApiUserGetAllInput : IRequest<List<ApiUserModel>>
    {
    }
}
