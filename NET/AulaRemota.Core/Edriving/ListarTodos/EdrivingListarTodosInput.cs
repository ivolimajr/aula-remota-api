using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Edriving.ListarTodos
{
    public class EdrivingListarTodosInput : IRequest<List<EdrivingModel>>
    {
    }
}
