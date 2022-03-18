using AulaRemota.Infra.Entity;
using MediatR;

namespace AulaRemota.Core.PartnnerLevel.GetOne
{
    public class PartnnerLevelGetOneInput : IRequest<PartnnerLevelModel>
    {
        public int Id { get; set; }
    }
}
