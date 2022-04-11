using AulaRemota.Core.Partnner.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AulaRemota.Core.Instructor.Create;
using AulaRemota.Core.File.DownloadFromAzure;
using AulaRemota.Core.Instructor.GetAll;
using AulaRemota.Core.Instructor.GetOne;
using System.Collections.Generic;
using AulaRemota.Core.Instructor.Update;
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Core.Instructor.Remove;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints para gerenciar os usuários dos Instrutor - DETRANS
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorController(IMediator mediator) =>
            _mediator = mediator;

        /// <summary>
        /// Retorna um Array de items com os usuários do tipo Instrutor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<InstructorModel>), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<InstructorModel>>> GetAll([FromQuery] string uf) =>
            Ok(await _mediator.Send(new InstructorGetAllInput() { Uf = uf }));

        /// <summary>
        /// Retorna um item com o parceiro solicitado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InstructorModel), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<InstructorGetOneInput>> Get(int id, [FromQuery] string uf) =>
            Ok(await _mediator.Send(new InstructorGetOneInput { Id = id, Uf = uf }));

        /// <summary>
        /// Insere um novo usuário do tipo Instrutor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(InstructorModel), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Post([FromForm] InstructorCreateInput request) =>
            StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));

        /// <summary>
        /// Atualiza uma Instrutor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PartnnerUpdateResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Put([FromForm] InstructorUpdateInput request) => StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));

        /// <summary>
        /// Remove um usuário do tipo Instrutor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id) => Ok(await _mediator.Send(new InstructorRemoveInput { Id = id }));

        /// <summary>
        /// Remove um parceiro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadFile")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<string>> ArquivoDownload(DownloadFileFromAzureInput request) =>
            await _mediator.Send(new DownloadFileFromAzureInput { FileName = request.FileName, TypeUser = Constants.Roles.INSTRUTOR });
    }
}