using MediatR;

namespace AulaRemota.Core.Parceiro.Deletar
{
    public class ParceiroDeletarInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
