using MediatR;

namespace AulaRemota.Core.Parceiro.ListarTodos
{
    public class ParceiroListarPorIdInput : IRequest<ParceiroListarPorIdResponse>
    {
        public int Id { get; set; }
    }
}
