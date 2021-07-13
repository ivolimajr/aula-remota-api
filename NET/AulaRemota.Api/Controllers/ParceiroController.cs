using AulaRemota.Api.Models.Requests;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    //[Authorize("Bearer")]
    [Route("api/[controller]")]
    public class ParceiroController : ControllerBase
    {
        private readonly ILogger<ParceiroController> _logger;
        private readonly IParceiroServices _parceiroService;

        public ParceiroController(ILogger<ParceiroController> logger, IParceiroServices parceiroServices)
        {
            _logger = logger;
            _parceiroService = parceiroServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _parceiroService.GetAllWithRelationship();

            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _parceiroService.GetById(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ParceiroCreateRequest parceiro)
        {
            if (!_parceiroService.ValidateEntity(parceiro)) return BadRequest("Invalid values");

            var result = _parceiroService.Create(parceiro);

            if (result != null) return Ok(result);

            return BadRequest("Email já registrado");

        }

        [HttpPut]
        public IActionResult Put([FromBody] ParceiroCreateRequest parceiro)
        {
            if (parceiro.Id == 0 ) return BadRequest("Invalid values");
            if (!_parceiroService.ValidateEntity(parceiro)) return BadRequest("Invalid values");

            var result = _parceiroService.Update(parceiro);
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id == 0) return BadRequest("Invalid values");
            
            _parceiroService.Delete(id);

            return Ok();
        }

        [HttpPost]
        [Route("inativar")]
       public IActionResult Inativar([FromQuery] int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _parceiroService.Inativar(id);
            if (!result) return NoContent();

            return Ok();
        }

        [HttpPost]
        [Route("ativar")]
       public IActionResult Ativar([FromQuery] int id)
        {
            if (id == 0) return BadRequest("Invalid values");

            var result = _parceiroService.Ativar(id);
            if (!result) return NoContent();

            return Ok();
        }


    }
}
