using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.ListarTodos
{
    public class AutoEscolaListarTodosHandler : IRequestHandler<AutoEscolaListarTodosInput, List<AutoEscolaModel>>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;

        public AutoEscolaListarTodosHandler(IRepository<AutoEscolaModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<List<AutoEscolaModel>> Handle(AutoEscolaListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                return _autoEscolaRepository.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
