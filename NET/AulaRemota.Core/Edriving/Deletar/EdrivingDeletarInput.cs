using MediatR;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarInput : IRequest<string>
    {
        public int Id { get; set; }
    }
}
