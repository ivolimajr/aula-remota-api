using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.EdrivingLevel.GetAll
{
    public class EdrivingLevelGetAllInput : IRequest<List<EdrivingLevelModel>>
    {
    }
}
