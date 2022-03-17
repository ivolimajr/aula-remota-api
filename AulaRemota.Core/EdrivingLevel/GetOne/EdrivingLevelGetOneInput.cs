using AulaRemota.Infra.Entity;
using MediatR;

namespace AulaRemota.Core.EdrivingLevel.GetOne
{
    public class EdrivingLevelGetOneInput : IRequest<EdrivingLevelModel>
    {
        public int Id { get; set; }
    }
}
