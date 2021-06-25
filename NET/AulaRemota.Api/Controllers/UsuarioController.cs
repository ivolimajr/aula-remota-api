using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioServices _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioServices usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _usuarioService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            var result = _usuarioService.Create(usuario);

            if (result != null) return Ok(result);

            return NotFound();

        }

        [HttpPost]
        [Route("login")]
        public IActionResult Post([FromQuery] string email, string senha)
        {
            if (email == string.Empty || senha == string.Empty) return BadRequest();

            var result = _usuarioService.Login(email,senha);
            if (result == null) return NotFound();

            result.Password = null;

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioService.Delete(id);
            return NoContent();
        }
    }
}
