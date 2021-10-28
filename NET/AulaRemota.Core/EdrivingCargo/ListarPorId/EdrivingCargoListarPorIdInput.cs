using AulaRemota.Infra.Entity;
using MediatR;

namespace AulaRemota.Core.EdrivingCargo.ListarPorId
{
    public class EdrivingCargoListarPorIdInput : IRequest<EdrivingCargoModel>
    {
        public int Id { get; set; }
    }
}
