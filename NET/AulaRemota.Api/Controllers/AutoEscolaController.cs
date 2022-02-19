using AulaRemota.Core.Partnner.Update;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AulaRemota.Core.DrivingSchool.Create;
using AulaRemota.Core.File.DownloadFromAzure;
using AulaRemota.Core.DrivingSchool.GetAll;
using AulaRemota.Core.DrivingSchool.Remove;
using AulaRemota.Core.DrivingSchool.GetOne;
using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;
using AulaRemota.Core.DrivingSchool.Update;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints para gerenciar os usuários dos Auto Escola - DETRANS
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AutoEscolaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutoEscolaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna um Array de items com os usuários do tipo Auto Escola
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<DrivingSchoolModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<List<DrivingSchoolModel>>> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new DrivingSchoolGetAllInput()));
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Retorna um item com o parceiro solicitado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DrivingSchoolGetOneInput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<DrivingSchoolGetOneInput>> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new DrivingSchoolGetOneInput { Id = id});
                return Ok(result);
            }
            catch (CustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Insere um novo usuário do tipo Auto Escola
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DrivingSchoolCreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromForm] DrivingSchoolCreateInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));
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
        /// Atualiza uma auto escola
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PartnnerUpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Put([FromForm] DrivingSchoolUpdateInput request)
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
        /// <summary>
        /// Remove um usuário do tipo Auto Escola
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DrivingSchoolRemoveInput { Id = id });
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
        /// Remove um parceiro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ArquivoDownload")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<string> ArquivoDownload(DownloadFileFromAzureInput request)
        {
                var result = await _mediator.Send(new DownloadFileFromAzureInput { NomeArquivo = request.NomeArquivo });
                return result;
        }
    }
}