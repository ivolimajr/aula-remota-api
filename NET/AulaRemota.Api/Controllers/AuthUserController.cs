using AulaRemota.Core.AuthUser.Criar;
using AulaRemota.Core.AuthUser.Login;
using AulaRemota.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AulaRemota.Api.Controllers
{
    /// <summary>
    /// Lista os EndPoints para obter acesso a API.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todos os usuário com acesso a API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(AuthUserListarTodosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new AuthUserListarTodosInput()));
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
        /// Retorna um usuário informando o ID como parâmetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuthUserListarPorIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new AuthUserListarPorIdInput { Id = id });
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
        /// Cadastra um novo usuário para consumir a API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(AuthUserCriarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromBody] AuthUserCriarInput request)
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
        /// Atualiza um usuário da API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(AuthUserAtualizarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> Put([FromBody] AuthUserAtualizarInput request)
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
        /// Remove um usuário da API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new AuthUserDeletarInput { Id = id });
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
