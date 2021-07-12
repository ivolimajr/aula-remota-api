using AulaRemota.Core.Entity.Auth;
using AulaRemota.Core.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AulaRemota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private IAuthUserServices _authUserServices;

        public AuthUserController(IAuthUserServices authUserServices)
        {
            _authUserServices = authUserServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _authUserServices.GetAll();
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _authUserServices.GetById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthUserRequest usuario)
        {
            if (usuario == null) return BadRequest();

            var result = _authUserServices.Create(usuario);
            if (result != null) return Ok(result);

            return NotFound();

        }

        [HttpPut]
        public IActionResult Put([FromBody] AuthUserRequest usuario)
        {
            if (!_authUserServices.ValidateEntity(usuario)) return BadRequest("Invalid values");

            var result = _authUserServices.Update(usuario);
            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _authUserServices.Delete(id);
            if (!result) return NoContent();

            return Ok();
        }
    }
}
