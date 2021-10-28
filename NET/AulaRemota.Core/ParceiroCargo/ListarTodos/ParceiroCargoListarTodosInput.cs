using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.ParceiroCargo.ListarTodos
{
    public class ParceiroCargoListarTodosInput : IRequest<List<ParceiroCargoModel>>
    {
    }
}
