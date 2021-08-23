﻿using AulaRemota.Core.EdrivingCargo.ListarPorId;
using AulaRemota.Core.EdrivingCargo.ListarTodos;
using AulaRemota.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints de retorno, lista apenas os cargos referente aos usuários responsáveis pelo Edriving
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EdrivingCargoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EdrivingCargoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(EdrivingCargoListarTodosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new EdrivingCargoListarTodosInput()));
            }
            catch (HttpClientCustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Retorna apenas um
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EdrivingCargoListarPorIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new EdrivingCargoListarPorIdInput { Id = id});
                return Ok(result);
            }
            catch (HttpClientCustomException e)
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
