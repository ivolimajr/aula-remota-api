using MediatR;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
