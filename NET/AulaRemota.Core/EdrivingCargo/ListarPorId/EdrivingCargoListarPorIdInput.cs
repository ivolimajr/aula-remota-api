using MediatR;

namespace AulaRemota.Core.EdrivingCargo.ListarPorId
{
    public class EdrivingCargoListarPorIdInput : IRequest<EdrivingCargoListarPorIdResponse>
    {
        public int Id { get; set; }
    }
}
