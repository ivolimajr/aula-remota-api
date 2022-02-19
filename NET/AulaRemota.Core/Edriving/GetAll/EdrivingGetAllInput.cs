using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Edriving.GetAll
{
    public class EdrivingGetAllInput : IRequest<List<EdrivingModel>>
    {
    }
}
