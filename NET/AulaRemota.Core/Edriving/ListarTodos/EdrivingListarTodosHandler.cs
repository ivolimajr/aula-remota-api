using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.ListarTodos
{
    public class EdrivingListarTodosHandler : IRequestHandler<EdrivingListarTodosInput, List<EdrivingModel>>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;

        public EdrivingListarTodosHandler(IRepository<EdrivingModel> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<List<EdrivingModel>> Handle(EdrivingListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                return await _edrivingRepository.Context
                    .Set<EdrivingModel>()
                    .Include(u => u.Usuario)
                    .Include(c => c.Cargo)
                    .Include(t => t.Telefones)
                    .ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
