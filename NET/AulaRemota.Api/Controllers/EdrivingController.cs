using AulaRemota.Core.Edriving.Atualizar;
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
    /// <summary>g
    /// Lista os EndPoints para gerenciar os usuários do Edriving
    /// </summary>
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

        /// <summary>
        /// Retorna um Array de items com os usuários
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna um item com o usuário solicitado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Insere um novo usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
