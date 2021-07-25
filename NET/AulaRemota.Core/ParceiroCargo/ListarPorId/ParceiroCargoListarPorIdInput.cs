using MediatR;

namespace AulaRemota.Core.ParceiroCargo.ListarPorId
{
    public class ParceiroCargoListarPorIdInput : IRequest<ParceiroCargoListarPorIdResponse>
    {
        public int Id { get; set; }
    }
}
