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

        /*
            Exibe todos os cargos do Edriving
            Se o retorno for Null siginifica que não exite valores no banco
         */
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _edrivingCargoService.GetAll();
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var cargo = _edrivingCargoService.GetById(id);
            if (cargo == null) return NotFound();

            return Ok(cargo);
        }

        /*
            Cria um novo cargo
            Recebe body no formato Json:
            int id
            string cargo
         */
        [HttpPost]
        public IActionResult Post([FromBody] EdrivingCargo cargo)
        {
            if (cargo.Cargo == null) return BadRequest("Invalid values");

            var result = _edrivingCargoService.Create(cargo);
            if (result == null) return Problem("processing error");

            return Created("Sucesso", result);

        }

        [HttpPut]
        public IActionResult Put([FromBody] EdrivingCargo cargo)
        {
            if (cargo == null || cargo.Id == 0) return BadRequest("Invalid values");

            var result = _edrivingCargoService.Update(cargo);
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _edrivingCargoService.Delete(id);
            if (!result) return NoContent();

            return Ok();
        }

    }
}
