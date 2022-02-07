using AulaRemota.Core.ApiAuth.GenerateToken;
using AulaRemota.Core.ApiAuth.RefreshToken;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AulaRemota.Api.Controllers
{
    /// <summary>
    /// Endpoints para obter o token e refreshToken
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obter o token para consumo da API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o Token bearer</response>
        /// <response code="401">Provavelmente você não tem acesso a API</response>
        [HttpPost]
        [Route("getToken")]
        [ProducesResponseType(typeof(GenerateTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult> GetToken([FromBody] GenerateTokenInput request)
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
        /// Atualizar o Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Atualiza o Token</response>
        /// <response code="404">RefreshToken inválido</response>
        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult> Refresh([FromBody] RefreshTokenInput request)
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
    }
}
