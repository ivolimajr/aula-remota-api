using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
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
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _usuarioService.GetById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario == null) return BadRequest();

            var result = _usuarioService.Create(usuario);
            if (result != null) return Ok(result);

            return NotFound();

        }

        [HttpPut]
        public IActionResult Put([FromBody] Usuario usuario)
        {
            if (!_usuarioService.ValidateEntity(usuario)) return BadRequest("Invalid values");

            var result = _usuarioService.Update(usuario);
            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Post([FromBody] Login login)
        {
            if (login.Email == null || login.Password == null) return BadRequest(new
            {
                success = false,
                error = "Verifique os dados"
            });
            if (login.Email == string.Empty || login.Password == string.Empty) return BadRequest(new
            {
                success = false,
                error = "Verifique os dados"
            });

            var result = _usuarioService.Login(login.Email, login.Password);
            if (result == null) return BadRequest(new
            {
                success = false,
                error = "Credenciais não encontradas"
            });

            result.Password = null;

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _usuarioService.Delete(id);
            if (!result) return NoContent();

            return Ok();
        }
    }
}
