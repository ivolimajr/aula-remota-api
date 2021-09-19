using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.ListarTodos
{
    public class AutoEscolaListarTodosHandler : IRequestHandler<AutoEscolaListarTodosInput, AutoEscolaListarTodosResponse>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;

        public AutoEscolaListarTodosHandler(IRepository<AutoEscolaModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<AutoEscolaListarTodosResponse> Handle(AutoEscolaListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _autoEscolaRepository.GetAll().ToList();

                return new AutoEscolaListarTodosResponse { Items = result };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
