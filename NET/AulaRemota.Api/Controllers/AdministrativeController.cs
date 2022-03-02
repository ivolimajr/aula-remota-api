using AulaRemota.Core.Administrative.Create;
using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ProducesResponseType(typeof(AdministrativeModel), StatusCodes.Status201Created)]
        public async ValueTask<ActionResult> Administrative(AdministrativeCreateInput administrative) =>
               StatusCode(StatusCodes.Status201Created, await _mediator.Send(administrative));
    }
}
