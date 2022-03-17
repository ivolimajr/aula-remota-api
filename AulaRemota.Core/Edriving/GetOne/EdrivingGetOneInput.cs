using AulaRemota.Infra.Entity;
using MediatR;

namespace AulaRemota.Core.Edriving.GetOne
{
    public class EdrivingGetOneInput : IRequest<EdrivingModel>
    {
        public int Id { get; set; }
    }
}
