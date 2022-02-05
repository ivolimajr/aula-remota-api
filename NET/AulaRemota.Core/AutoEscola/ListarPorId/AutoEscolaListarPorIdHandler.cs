using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.ListarPorId
{
    public class AutoEscolaListarPorIdHandler : IRequestHandler<AutoEscolaListarPorIdInput, AutoEscolaListarPorIdResponse>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;

        public AutoEscolaListarPorIdHandler(IRepository<AutoEscolaModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<AutoEscolaListarPorIdResponse> Handle(AutoEscolaListarPorIdInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var result = await _autoEscolaRepository.Context.Set<AutoEscolaModel>()
                    .Include(e => e.Telefones)
                    .Include(e => e.Endereco)
                    .Include(e => e.Arquivos)
                    .Include(e => e.Usuario)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                if(result == null) throw new HttpClientCustomException("Não encontrado");


                return new AutoEscolaListarPorIdResponse
                {
                    Id = result.Id,
                    RazaoSocial = result.RazaoSocial,
                    NomeFantasia = result.NomeFantasia,
                    InscricaoEstadual = result.InscricaoEstadual,
                    DataFundacao = result.DataFundacao,
                    Email = result.Email,
                    Descricao = result.Descricao,
                    Site = result.Site,
                    Cnpj = result.Cnpj,
                    EnderecoId = result.EnderecoId,
                    Endereco = result.Endereco,
                    UsuarioId = result.UsuarioId,
                    Usuario = result.Usuario,
                    Arquivos = result.Arquivos,
                    Telefones = result.Telefones
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
