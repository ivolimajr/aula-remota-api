using AulaRemota.Infra.Entity;
using MediatR;

namespace AulaRemota.Core.PartnnerCargo.ListarPorId
{
    public class ParceiroCargoListarPorIdInput : IRequest<PartnnerLevelModel>
    {
        public int Id { get; set; }
    }
}
