using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace AulaRemota.Core.Partnner.GetOne
{
    public class GetOnePartnnerHandler : IRequestHandler<GetOnePartnnerInput, GetOnePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel> _parceiroRepository;

        public GetOnePartnnerHandler(IRepository<PartnnerModel> parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<GetOnePartnnerResponse> Handle(GetOnePartnnerInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var res = await _parceiroRepository.GetByIdAsync(request.Id);
                if (res == null) throw new CustomException("Não Encontrado");

                var result = await _parceiroRepository.Context
                        .Set<PartnnerModel>()
                        .Include(e => e.Usuario)
                        .Include(e => e.Cargo)
                        .Include(e => e.Endereco)
                        .Include(e => e.Telefones)
                        .Where(e => e.Id == request.Id)
                        .Where(e => e.Usuario.status > 0)
                        .FirstAsync();

                return new GetOnePartnnerResponse {
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
