using MediatR;

namespace AulaRemota.Core.AutoEscola.Deletar
{
    public class AutoEscolaDeletarInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
