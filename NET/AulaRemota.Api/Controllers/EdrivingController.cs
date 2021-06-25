using AulaRemota.Api.Models.Requests;
using AulaRemota.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdrivingController : ControllerBase
    {
        private readonly ILogger<EdrivingController> _logger;
        private readonly IEdrivingServices _edrivingService;

        public EdrivingController(ILogger<EdrivingController> logger, IEdrivingServices EdrivingService)
        {
            _logger = logger;
            _edrivingService = EdrivingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _edrivingService.GetAllWithRelationship();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _edrivingService.GetById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EdrivingCreateRequest Edriving)
        {
            if (Edriving == null)   return BadRequest();

            var result = _edrivingService.Create(Edriving);

            if (result != null) return Ok(result);

            return NotFound();

        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            _edrivingService.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("inativar")]
       public IActionResult Inativar([FromQuery] int id)
        {
            _edrivingService.Inativar(id);
            return Ok();
        }

        [HttpPost]
        [Route("ativar")]
       public IActionResult Ativar([FromQuery] int id)
        {
            _edrivingService.Ativar(id);
            return Ok();
        }


    }
}
