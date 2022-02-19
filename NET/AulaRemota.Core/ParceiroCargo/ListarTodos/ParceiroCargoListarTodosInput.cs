using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.PartnnerCargo.ListarTodos
{
    public class ParceiroCargoListarTodosInput : IRequest<List<PartnnerLevelModel>>
    {
    }
}
