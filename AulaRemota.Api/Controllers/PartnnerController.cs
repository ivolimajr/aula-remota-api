using AulaRemota.Core.Partnner.Update;
using AulaRemota.Core.Partnner.Remove;
using AulaRemota.Core.Partnner.GetAll;
using AulaRemota.Core.Partnner.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using AulaRemota.Infra.Entity;
using AulaRemota.Core.Partnner.GetOne;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints para gerenciar os usuários dos Parceiros - DETRANS
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PartnnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartnnerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna um Array de items com os parceiros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PartnnerModel>), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<PartnnerModel>>> GetAll() => Ok(await _mediator.Send(new GetAllPartnnerInput()));

        /// <summary>
        /// Retorna um item com o parceiro solicitado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOnePartnnerResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<GetOnePartnnerResponse>> Get(int id) => Ok(await _mediator.Send(new GetOnePartnnerInput { Id = id }));

        /// <summary>
        /// Insere um novo parceiro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePartnnerResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Post([FromBody] CreatePartnnerInput request) => StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));

        /// <summary>
        /// Atualiza um parceiro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PartnnerUpdateResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Put([FromBody] PartnnerUpdateInput request) => Ok(await _mediator.Send(request));
        /// <summary>
        /// Remove um parceiro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id) => Ok(await _mediator.Send(new RemovePartnnerInput { Id = id }));
    }
}