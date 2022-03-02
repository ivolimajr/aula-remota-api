using MediatR;

namespace AulaRemota.Core.Administrative.Remove
{
    public class AdministrativeRemoveInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
