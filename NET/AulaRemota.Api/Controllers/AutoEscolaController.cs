using AulaRemota.Core.Parceiro.Atualizar;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AulaRemota.Core.AutoEscola.Create;
using AulaRemota.Core.Arquivo.Download;
using AulaRemota.Core.AutoEscola.GetAll;
using AulaRemota.Core.AutoEscola.Remove;
using AulaRemota.Core.AutoEscola.GetOne;
using AulaRemota.Infra.Entity.Auto_Escola;
using System.Collections.Generic;
using AulaRemota.Core.AutoEscola.Update;

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
        [ProducesResponseType(typeof(List<AutoEscolaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<List<AutoEscolaModel>>> GetAll()
        {
            try
            {
                return Ok(await _mediator.Send(new AutoEscolaGetAllInput()));
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
        [ProducesResponseType(typeof(AutoEscolaGetOneInput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<AutoEscolaGetOneInput>> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new AutoEscolaGetOneInput { Id = id});
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
        [ProducesResponseType(typeof(AutoEscolaCreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromForm] AutoEscolaCreateInput request)
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
        [ProducesResponseType(typeof(ParceiroAtualizarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Put([FromForm] AutoEscolaUpdateInput request)
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
                var result = await _mediator.Send(new AutoEscolaRemoveInput { Id = id });
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
        public async ValueTask<string> ArquivoDownload(ArquivoDownloadInput request)
        {
                var result = await _mediator.Send(new ArquivoDownloadInput { NomeArquivo = request.NomeArquivo });
                return result;
        }
    }
}