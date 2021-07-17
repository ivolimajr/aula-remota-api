using AulaRemota.Core.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                var res = await _edrivingRepository.GetByIdAsync(request.Id);
                if (res == null) throw new HttpClientCustomException("Não Encontrado");

                var result = await _edrivingRepository.Context
                        .Set<EdrivingModel>()
                        .Include(u => u.Usuario)
                        .Include(c => c.Cargo)
                        .Where(u => u.Id == request.Id)
                        .Where(u => u.Usuario.status > 0)
                        .FirstAsync();

                return new EdrivingListarPorIdResponse { 
                
                    Id = result.Id,
                    FullName = result.FullName,
                    Email = result.Email,
                    Cpf = result.Cpf,
                    Telefone = result.Telefone,
                    CargoId = result.CargoId,
                    Cargo = result.Cargo,
                    UsuarioId= result.UsuarioId,
                    Usuario= result.Usuario
                };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
