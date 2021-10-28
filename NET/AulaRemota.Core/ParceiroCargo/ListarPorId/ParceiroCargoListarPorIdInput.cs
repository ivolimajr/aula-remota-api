using AulaRemota.Infra.Entity;
using MediatR;

namespace AulaRemota.Core.ParceiroCargo.ListarPorId
{
    public class ParceiroCargoListarPorIdInput : IRequest<ParceiroCargoModel>
    {
        public int Id { get; set; }
    }
}
