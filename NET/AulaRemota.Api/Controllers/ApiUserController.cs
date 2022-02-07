using AulaRemota.Core.ApiUser.Update;
using AulaRemota.Core.ApiUser.Create;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AulaRemota.Core.ApiUser.GetAll;
using AulaRemota.Core.ApiUser.GetOne;
using AulaRemota.Core.ApiUser.Remove;
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Api.Controllers
{
    /// <summary>
    /// Lista os EndPoints para obter acesso a API.
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [Authorize(Roles = Constants.Roles.APIUSER)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todos os usuário com acesso a API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiUserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<List<ApiUserModel>>> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new ApiUserGetAllInput()));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.ResponseModel.UserMessage,
                                statusCode: (int)e.ResponseModel.StatusCode,
                                type: e.ResponseModel.ModelName);
            }
        }

        /// <summary>
        /// Retorna um usuário informando o ID como parâmetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiUserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<ApiUserModel>> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new ApiUserGetOneInput { Id = id });
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
        /// Cadastra um novo usuário para consumir a API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ApiUserCreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromBody] ApiUserCreateInput request)
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
        /// Atualiza um usuário da API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiUserUpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> Put([FromBody] ApiUserUpdateInput request)
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
        /// Remove um usuário da API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new ApiUserRemoveInput { Id = id });
                return Ok(result);
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
