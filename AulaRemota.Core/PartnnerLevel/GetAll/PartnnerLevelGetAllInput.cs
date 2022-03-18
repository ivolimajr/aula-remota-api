using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.PartnnerLevel.GetAll
{
    public class PartnnerLevelGetAllInput : IRequest<List<PartnnerLevelModel>>
    {
    }
}
