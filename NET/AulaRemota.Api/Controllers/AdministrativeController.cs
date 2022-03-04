using AulaRemota.Core.Administrative.Create;
using AulaRemota.Core.Administrative.GetAll;
using AulaRemota.Core.Administrative.GetOne;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AulaRemota.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [ApiController]
    public class AdministrativeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministrativeController(IMediator mediator)=> 
            _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(List<AdministrativeModel>), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> GetAll() =>
            StatusCode(StatusCodes.Status200OK, await _mediator.Send(new AdministrativeGetAllInput()));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AdministrativeModel), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<AdministrativeModel>> Get(int id) => Ok(await _mediator.Send(new AdministrativeGetOneInput { Id = id }));

        [HttpPost]
        [ProducesResponseType(typeof(AdministrativeModel), StatusCodes.Status201Created)]
        public async ValueTask<ActionResult> Administrative(AdministrativeCreateInput administrative) =>
               StatusCode(StatusCodes.Status201Created, await _mediator.Send(administrative));
    }
}
