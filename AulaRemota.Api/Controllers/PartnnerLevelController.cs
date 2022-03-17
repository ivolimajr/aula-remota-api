using AulaRemota.Core.PartnnerLevel.GetOne;
using AulaRemota.Core.PartnnerLevel.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints de retorno, lista apenas os cargos referente aos parceiros
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PartnnerLevelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartnnerLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PartnnerLevelModel>), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<PartnnerLevelModel>>> GetAll() => Ok(await _mediator.Send(new PartnnerLevelGetAllInput()));

        /// <summary>
        /// Retorna apena um
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PartnnerLevelModel), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<PartnnerLevelModel>> Get(int id) => Ok(await _mediator.Send(new PartnnerLevelGetOneInput { Id = id }));
    }
}