using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Parceiro.ListarTodos
{
    public class ParceiroListarTodosHandler : IRequestHandler<ParceiroListarTodosInput, List<ParceiroModel>>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;

        public ParceiroListarTodosHandler(IRepository<ParceiroModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<List<ParceiroModel>> Handle(ParceiroListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                return await _parceiroRepository.Context
                    .Set<ParceiroModel>()
                    .Include(e => e.Usuario)
                    .Include(e => e.Cargo)
                    .Include(e => e.Endereco)
                    .Include(e => e.Telefones)
                    .Where(e => e.Usuario.status > 0)
                    .ToListAsync(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
