using MediatR;

namespace AulaRemota.Core.Edriving.ListarTodos
{
    public class EdrivingListarPorIdInput : IRequest<EdrivingListarPorIdResponse>
    {
        public int Id { get; set; }
    }
}
