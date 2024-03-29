﻿using AulaRemota.Core.EdrivingLevel.GetOne;
using AulaRemota.Core.EdrivingLevel.GetAll;
using AulaRemota.Infra.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AulaRemota.Api.Controllers
{
    /// <summary>g
    /// Lista os EndPoints de retorno, lista apenas os cargos referente aos usuários responsáveis pelo Edriving
    /// </summary>
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EdrivingLevelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EdrivingLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retorna todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EdrivingLevelModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<List<EdrivingLevelModel>>> GetAll() => Ok(await _mediator.Send(new EdrivingLevelGetAllInput()));

        /// <summary>
        /// Retorna apenas um
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EdrivingLevelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult<EdrivingLevelModel>> Get(int id) => Ok(await _mediator.Send(new EdrivingLevelGetOneInput { Id = id }));
    }
}
