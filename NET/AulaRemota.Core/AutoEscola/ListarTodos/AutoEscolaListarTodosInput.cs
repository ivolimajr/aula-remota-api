using AulaRemota.Infra.Entity.Auto_Escola;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.AutoEscola.ListarTodos
{
    public class AutoEscolaListarTodosInput : IRequest<List<AutoEscolaModel>>
    {
    }
}
