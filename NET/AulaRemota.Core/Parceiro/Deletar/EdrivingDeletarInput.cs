using MediatR;

namespace AulaRemota.Core.Parceiro.Deletar
{
    public class ParceiroDeletarInput : IRequest<string>
    {
        public int Id { get; set; }
    }
}
