﻿using AulaRemota.Core.Edriving.Atualizar;
using AulaRemota.Core.Edriving.Deletar;
using AulaRemota.Core.Edriving.ListarTodos;
using AulaRemota.Core.Edriving.Criar;
using AulaRemota.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AulaRemota.Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class EdrivingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EdrivingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EdrivingListarTodosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new EdrivingListarTodosInput()));
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


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EdrivingListarPorIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new EdrivingListarPorIdInput { Id = id});
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


        [HttpPost]
        [ProducesResponseType(typeof(EdrivingCriarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromBody] EdrivingCriarInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));
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

        [HttpPut]
        [ProducesResponseType(typeof(EdrivingAtualizarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Put([FromBody] EdrivingAtualizarInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
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

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new EdrivingDeletarInput { Id = id });
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
