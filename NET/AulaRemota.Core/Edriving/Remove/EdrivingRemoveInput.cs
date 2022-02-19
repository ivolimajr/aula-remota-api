using MediatR;

namespace AulaRemota.Core.Edriving.Remove
{
    public class EdrivingRemoveInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
