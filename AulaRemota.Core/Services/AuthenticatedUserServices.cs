using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AulaRemota.Core.Services
{
    public class AuthenticatedUserServices
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUserServices(IHttpContextAccessor accessor) => _accessor = accessor;
        public string Email => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetRoles() => _accessor.HttpContext.User.Claims.Where(e => e.Type == ClaimTypes.Role).ToList();
    }
}
