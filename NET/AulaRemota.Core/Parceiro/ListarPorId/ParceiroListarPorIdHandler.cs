using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Parceiro.ListarTodos
{
    public class ParceiroListarPorIdHandler : IRequestHandler<ParceiroListarPorIdInput, ParceiroListarPorIdResponse>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;

        public ParceiroListarPorIdHandler(IRepository<ParceiroModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<ParceiroListarPorIdResponse> Handle(ParceiroListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var res = await _parceiroRepository.GetByIdAsync(request.Id);
                if (res == null) throw new HttpClientCustomException("Não Encontrado");

                var result = await _parceiroRepository.Context
                        .Set<ParceiroModel>()
                        .Include(u => u.Usuario)
                        .Include(c => c.Cargo)
                        .Include(e => e.Endereco)
                        .Where(u => u.Id == request.Id)
                        .Where(u => u.Usuario.status > 0)
                        .FirstAsync();

                return new ParceiroListarPorIdResponse {
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    Telefones = result.Telefones,
                    Descricao = result.Descricao,
                    Cnpj = result.Cnpj,
                    CargoId = result.CargoId,
                    Cargo = result.Cargo,
                    EnderecoId = result.EnderecoId,
                    Endereco = result.Endereco,
                    UsuarioId = result.UsuarioId,
                    Usuario = result.Usuario,
                };
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
