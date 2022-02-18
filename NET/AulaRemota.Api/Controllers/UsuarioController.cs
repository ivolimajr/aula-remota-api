using AulaRemota.Shared.Helpers;
using AulaRemota.Core.Usuario.AtualizarEndereco;
using AulaRemota.Core.Usuario.AtualizarSenha;
using AulaRemota.Core.Usuario.AtualizarSenhaPorEmail;
using AulaRemota.Core.Usuario.Login;
using AulaRemota.Core.Usuario.RemoveTelefone;
using AulaRemota.Infra.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AulaRemota.Core.Usuario.RemoveFile;

namespace AulaRemota.Api.Controllers
{
    /// <summary>
    /// Lista os EndPoints para gerenciar o usuário, tanto Edriving, auto escola, parceiro
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Endpoint para fazer login na plataforma
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Atualiza o Token</response>
        /// <response code="401">RefreshToken inválido</response>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UsuarioLoginResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Login([FromBody] UsuarioLoginInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.ResponseModel.UserMessage,
                                statusCode: (int)e.ResponseModel.StatusCode,
                                type: e.ResponseModel.ModelName);
            }
        }
        /// <summary>
        /// Endpoint alterar a senha do usuario pelo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Atualiza o Token</response>
        /// <response code="401">RefreshToken inválido</response>
        [HttpPost]
        [Route("alterar-senha")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult> AlterarSenha([FromBody] UsuarioAtualizarSenhaInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Endpoint alterar a senha do usuario pelo email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("alterar-senha-por-email")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult> AlterarSenhaPorEmail([FromBody] UsuarioAtualizarSenhaPorEmailInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Remove um arquivo de um usuário
        /// </summary>
        /// <param idArquivo="id do arquivo"></param>
        /// <returns></returns>
        [HttpDelete("RemoveArquivo/{id}")]
        public async ValueTask<ActionResult> RemoveArquivo(int id)
        {
            try
            {
                var result = await _mediator.Send(new RemoveFileInput { IdArquivo = id });
                return Ok(result);
            }
            catch (CustomException e)
            {
                return Problem(detail: e.ResponseModel.UserMessage,
                                statusCode: (int)e.ResponseModel.StatusCode,
                                type: e.ResponseModel.ModelName);
            }
        }
        /// <summary>
        /// Remove o telefone de um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Telefone/{id?}")]
        public async ValueTask<ActionResult> Telefone(int id)
        {
            try
            {
                var result = await _mediator.Send(new RemoveTelefoneInput { Id = id });
                return Ok(result);
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status204NoContent);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Atualizar endereço de um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("atualizar-endereco")]
        [HttpPut]
        [ProducesResponseType(typeof(EnderecoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> AtualizarEndereco([FromBody] UsuarioAtualizarEnderecoInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
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