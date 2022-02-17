using MediatR;

namespace AulaRemota.Core.AutoEscola.Remove
{
    public class AutoEscolaRemoveInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
