using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.GetAll
{
    public class AutoEscolaGetAllHandler : IRequestHandler<AutoEscolaGetAllInput, List<AutoEscolaModel>>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;

        public AutoEscolaGetAllHandler(IRepository<AutoEscolaModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<List<AutoEscolaModel>> Handle(AutoEscolaGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                //return _autoEscolaRepository.Context.Set<AutoEscolaModel>()
                //            .Include(e => e.Arquivos)
                //            .Include(e => e.Telefones)
                //            .Include(e => e.Endereco)
                //            .Include(e => e.Usuario)
                //            .ToList();
                return _autoEscolaRepository.GetAll().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
