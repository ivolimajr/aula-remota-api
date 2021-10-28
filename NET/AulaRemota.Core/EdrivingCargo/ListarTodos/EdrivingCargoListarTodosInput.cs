using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.EdrivingCargo.ListarTodos
{
    public class EdrivingCargoListarTodosInput : IRequest<List<EdrivingCargoModel>>
    {
    }
}
