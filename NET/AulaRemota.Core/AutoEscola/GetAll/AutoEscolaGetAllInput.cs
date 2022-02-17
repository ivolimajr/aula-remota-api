using AulaRemota.Infra.Entity.Auto_Escola;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.AutoEscola.GetAll
{
    public class AutoEscolaGetAllInput : IRequest<List<AutoEscolaModel>>
    {
    }
}
