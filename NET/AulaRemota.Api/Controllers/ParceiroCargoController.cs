using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
/*
namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class ParceiroCargoController : ControllerBase
    {
        private readonly ILogger<ParceiroCargoController> _logger;
        private readonly IParceiroCargoServices _parceiroCargoService;

        public ParceiroCargoController(ILogger<ParceiroCargoController> logger, IParceiroCargoServices parceiroCargoService)
        {
            _logger = logger;
            _parceiroCargoService = parceiroCargoService;
        }

        
         
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _parceiroCargoService.GetAll();
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var cargo = _parceiroCargoService.GetById(id);
            if (cargo == null) return NotFound();

            return Ok(cargo);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] ParceiroCargo cargo)
        {
            if (cargo.Cargo == null) return BadRequest("Invalid values");

            var result = _parceiroCargoService.Create(cargo);
            if (result == null) return Problem("processing error");

            return Created("Sucesso", result);

        }

        [HttpPut]
        public IActionResult Put([FromBody] ParceiroCargo cargo)
        {
            if (cargo == null || cargo.Id == 0) return BadRequest("Invalid values");

            var result = _parceiroCargoService.Update(cargo);
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _parceiroCargoService.Delete(id);
            if (!result) return NoContent();

            return Ok();
        }

    }
}
*/