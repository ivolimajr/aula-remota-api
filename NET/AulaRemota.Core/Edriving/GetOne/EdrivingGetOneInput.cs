using MediatR;

namespace AulaRemota.Core.Edriving.GetOne
{
    public class EdrivingGetOneInput : IRequest<EdrivingGetOneResponse>
    {
        public int Id { get; set; }
    }
}
