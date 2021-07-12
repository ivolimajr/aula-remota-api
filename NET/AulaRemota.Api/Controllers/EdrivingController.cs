using AulaRemota.Api.Models.Requests;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class EdrivingController : ControllerBase
    {
        private readonly ILogger<EdrivingController> _logger;
        private readonly IEdrivingServices _edrivingService;

        public EdrivingController(ILogger<EdrivingController> logger, IEdrivingServices edrivingService)
        {
            _logger = logger;
            _edrivingService = edrivingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _edrivingService.GetAll();
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest(new 
            {
                success = false,
                error = "Informe um valor"
            });

            var result = _edrivingService.GetById(id);
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EdrivingCreateRequest edriving)
        {
            if (!_edrivingService.ValidateEntity(edriving)) return BadRequest(new
            {
                success = false,
                error = "Valores Inválidos"
            });

            var result = _edrivingService.Create(edriving);
            if (result == null) return BadRequest(new
            {
                success = false,
                error = "Email já informado"
            });

            return Ok(result);

        }

        [HttpPut]
        public IActionResult Put([FromBody] EdrivingCreateRequest edriving)
        {
            if (edriving.Id == 0 ) return BadRequest("Invalid values");
            if (!_edrivingService.ValidateEntity(edriving)) return BadRequest(new
            {
                success = false,
                error = "Valores informados inválidos"
            });

            var result = _edrivingService.Update(edriving);
            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id == 0) return BadRequest(new
            {
                success = false,
                error = "Valor informados inválidos"
            });

            var result = _edrivingService.Delete(id);
            if (!result) return NoContent();

            return Ok();
        }

        [HttpPost]
        [Route("inativar")]
       public IActionResult Inativar([FromQuery] int id)
        {
            if (id == 0) return BadRequest(new
            {
                success = false,
                error = "Valor informados inválidos"
            });

            var result = _edrivingService.Inativar(id);
            if (!result) return NoContent();

            return Ok();
        }

        [HttpPost]
        [Route("ativar")]
       public IActionResult Ativar([FromQuery] int id)
        {
            if (id == 0) return BadRequest(new
            {
                success = false,
                error = "Valor informados inválidos"
            });

            var result = _edrivingService.Ativar(id);
            if (!result) return NoContent();

            return Ok();
        }


    }
}
