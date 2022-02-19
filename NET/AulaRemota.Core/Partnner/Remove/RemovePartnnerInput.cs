using MediatR;

namespace AulaRemota.Core.Partnner.Remove
{
    public class RemovePartnnerInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
