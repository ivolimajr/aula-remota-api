using AulaRemota.Core.PartnnerCargo.ListarPorId;
using AulaRemota.Core.PartnnerCargo.ListarTodos;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class ParceiroCargoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParceiroCargoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PartnnerLevelModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<List<PartnnerLevelModel>>> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new ParceiroCargoListarTodosInput()));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Retorna apena um
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PartnnerLevelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<PartnnerLevelModel>> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new ParceiroCargoListarPorIdInput { Id = id});
                return Ok(result);
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}