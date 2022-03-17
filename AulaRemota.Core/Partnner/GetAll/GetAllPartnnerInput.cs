using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Partnner.GetAll
{
    public class GetAllPartnnerInput : IRequest<List<PartnnerModel>>
    {
    }
}
