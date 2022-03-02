using AulaRemota.Core.Partnner.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints para gerenciar os usuários dos Auto Escola - DETRANS
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DrivingSchoolController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DrivingSchoolController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna um Array de items com os usuários do tipo Auto Escola
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<DrivingSchoolModel>), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<List<DrivingSchoolModel>>> GetAll([FromQuery] string uf) =>
            Ok(await _mediator.Send(new DrivingSchoolGetAllInput() { Uf = uf }));

        /// <summary>
        /// Retorna um item com o parceiro solicitado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DrivingSchoolModel), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<DrivingSchoolGetOneInput>> Get(int id) =>
            Ok(await _mediator.Send(new DrivingSchoolGetOneInput { Id = id }));

        /// <summary>
        /// Insere um novo usuário do tipo Auto Escola
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DrivingSchoolCreateResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Post([FromForm] DrivingSchoolCreateInput request) =>
            StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));

        /// <summary>
        /// Atualiza uma auto escola
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(PartnnerUpdateResponse), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult> Put([FromForm] DrivingSchoolUpdateInput request) => StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));

        /// <summary>
        /// Remove um usuário do tipo Auto Escola
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<ActionResult> Delete(int id) => Ok(await _mediator.Send(new DrivingSchoolRemoveInput { Id = id }));

        /// <summary>
        /// Remove um parceiro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DownloadFile")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async ValueTask<ActionResult<string>> ArquivoDownload(DownloadFileFromAzureInput request) =>
            await _mediator.Send(new DownloadFileFromAzureInput { FileName = request.FileName, TypeUser = Constants.Roles.AUTOESCOLA });
    }
}