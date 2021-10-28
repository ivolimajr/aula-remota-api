using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Parceiro.ListarTodos
{
    public class ParceiroListarTodosInput : IRequest<List<ParceiroModel>>
    {
    }
}
