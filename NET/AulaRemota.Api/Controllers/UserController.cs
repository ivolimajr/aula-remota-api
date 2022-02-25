using AulaRemota.Core.User.UpdateAddress;
using AulaRemota.Core.User.UpdatePassword;
using AulaRemota.Core.User.UpdatePasswordByEmail;
using AulaRemota.Core.User.Login;
using AulaRemota.Infra.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AulaRemota.Core.File.RemoveFile;
using AulaRemota.Core.User.RemovePhone;

namespace AulaRemota.Api.Controllers
{
    /// <summary>
    /// Lista os EndPoints para gerenciar o usuário, tanto Edriving, auto escola, parceiro
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
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
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Login([FromBody] UserLoginInput request) => StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));

        /// <summary>
        /// Endpoint alterar a senha do User pelo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Atualiza o Token</response>
        /// <response code="401">RefreshToken inválido</response>
        [HttpPost]
        [Route("alterar-senha")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> AlterarSenha([FromBody] UpdatePasswordInput request) => StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));

        /// <summary>
        /// Endpoint alterar a senha do User pelo email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("alterar-senha-por-email")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> AlterarSenhaPorEmail([FromBody] UpdatePasswordByEmailInput request) => StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));

        /// <summary>
        /// Remove um arquivo de um usuário
        /// </summary>
        /// <param idArquivo="id do arquivo"></param>
        /// <returns></returns>
        [HttpDelete("RemoveArquivo/{id}")]
        public async ValueTask<ActionResult> RemoveArquivo(int id) => Ok(await _mediator.Send(new RemoveFileInput { Id = id }));
        /// <summary>
        /// Remove o telefone de um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Telefone/{id?}")]
        public async ValueTask<ActionResult> Telefone(int id) => Ok(await _mediator.Send(new RemovePhoneInput { Id = id }));
        /// <summary>
        /// Atualizar endereço de um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("atualizar-endereco")]
        [HttpPut]
        [ProducesResponseType(typeof(AddressModel), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> AtualizarEndereco([FromBody] UserAddressUpdateInput request) => StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
    }
}