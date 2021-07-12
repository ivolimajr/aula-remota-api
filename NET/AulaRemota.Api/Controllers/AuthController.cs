using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AulaRemota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost]
        [Route("getToken")]
        public IActionResult GetToken([FromBody] GetTokenRequest user)
        {
            if (user == null) return BadRequest("Invalid Client Request");

            var token = _authServices.ValidateCredentials(user);
            if (token == null) return Unauthorized(); 

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] Token tokenRequest)
        {
            if (tokenRequest == null) return BadRequest("Invalid Client Request");

            var token = _authServices.ValidateCredentials(tokenRequest);

            if (token == null) return BadRequest("Invalid Client Request");

            return Ok(token);
        }

    }
}
