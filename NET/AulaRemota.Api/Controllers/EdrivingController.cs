using AulaRemota.Core.Edriving.Update;
using AulaRemota.Core.Edriving.Remove;
using AulaRemota.Core.Edriving.GetOne;
using AulaRemota.Core.Edriving.Create;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Collections.Generic;
using AulaRemota.Core.Edriving.GetAll;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints para gerenciar os usuários do Edriving
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EdrivingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EdrivingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna um Array de items com os usuários da plataforma
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EdrivingModel>), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<EdrivingModel>>> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new EdrivingGetAllInput()));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.ResponseModel.UserMessage,
                                statusCode: (int)e.ResponseModel.StatusCode,
                                type: e.ResponseModel.ModelName);
            }
        }
        /// <summary>
        /// Retorna um item com o usuário solicitado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EdrivingGetOneResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<EdrivingGetOneResponse>> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new EdrivingGetOneInput { Id = id});
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
        /// Insere um novo usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EdrivingCreateResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Post([FromBody] EdrivingCreateInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.ResponseModel.UserMessage,
                                statusCode: (int)e.ResponseModel.StatusCode,
                                type: e.ResponseModel.ModelName);
            }
        }
        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(EdrivingUpdateResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Put([FromBody] EdrivingUpdateInput request)
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
        /// Remove um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new EdrivingRemoveInput { Id = id });
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