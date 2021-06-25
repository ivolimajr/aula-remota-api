using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdrivingCargoController : ControllerBase
    {
        private readonly ILogger<EdrivingCargoController> _logger;
        private readonly IEdrivingCargoServices _edrivingCargoService;

        public EdrivingCargoController(ILogger<EdrivingCargoController> logger, IEdrivingCargoServices EdrivingCargoService)
        {
            _logger = logger;
            _edrivingCargoService = EdrivingCargoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _edrivingCargoService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EdrivingCargo cargo)
        {
            if (cargo == null)   return BadRequest();

            var result = _edrivingCargoService.Create(cargo);

            if (result != null) return Ok(result);

            return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _edrivingCargoService.Delete(id);
            return NoContent();
        }

    }
}
