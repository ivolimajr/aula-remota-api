using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.Edriving.ListarTodos
{
    public class EdrivingListarPorIdHandler : IRequestHandler<EdrivingListarPorIdInput, EdrivingListarPorIdResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;

        public EdrivingListarPorIdHandler(IRepository<EdrivingModel> edrivingRepository)
        {
            _edrivingRepository = edrivingRepository;
        }

        public async Task<EdrivingListarPorIdResponse> Handle(EdrivingListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var res = await _edrivingRepository.GetByIdAsync(request.Id);
                if (res == null) throw new HttpClientCustomException("Não Encontrado");

                var result = await _edrivingRepository.Context
                        .Set<EdrivingModel>()
                        .Include(e => e.Usuario)
                        .Include(e => e.Cargo)
                        .Include(e => e.Telefones)
                        .Where(e => e.Id == res.Id)
                        .Where(e => e.Usuario.status > 0)
                        .FirstOrDefaultAsync();

                return new EdrivingListarPorIdResponse { 
                
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    Cpf = result.Cpf,
                    Telefones = result.Telefones,
                    CargoId = result.CargoId,
                    Cargo = result.Cargo,
                    UsuarioId= result.UsuarioId,
                    Usuario= result.Usuario
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
