using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Parceiro.ListarTodos
{
    public class ParceiroListarTodosHandler : IRequestHandler<ParceiroListarTodosInput, ParceiroListarTodosResponse>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;

        public ParceiroListarTodosHandler(IRepository<ParceiroModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<ParceiroListarTodosResponse> Handle(ParceiroListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _parceiroRepository.Context
                    .Set<ParceiroModel>()
                    .Include(e => e.Usuario)
                    .Include(e => e.Cargo)
                    .Include(e => e.Endereco)
                    .Include(e => e.Telefones)
                    .Where(e => e.Usuario.status > 0)
                    .OrderBy(e => e.Id).ToListAsync();

                return new ParceiroListarTodosResponse { Items = result };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
