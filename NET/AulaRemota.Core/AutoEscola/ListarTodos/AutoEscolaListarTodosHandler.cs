using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.ListarTodos
{
    public class AutoEscolaListarTodosHandler : IRequestHandler<AutoEscolaListarTodosInput, List<AutoEscolaListarTodosResponse>>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;

        public AutoEscolaListarTodosHandler(IRepository<AutoEscolaModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<List<AutoEscolaListarTodosResponse>> Handle(AutoEscolaListarTodosInput request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _autoEscolaRepository.GetAll()
                                         .Select(e => new
                                         {
                                             e.Id,
                                             e.Email,
                                             e.RazaoSocial
                                         })
                                         .ToList();

                var autoEscolaList = new List<AutoEscolaListarTodosResponse>();

                foreach (var item in result)
                {
                    var res = new AutoEscolaListarTodosResponse
                    {
                        Id = item.Id,
                        Email = item.Email,
                        RazaoSocial = item.RazaoSocial
                    };
                    autoEscolaList.Add(res);
                }


                return autoEscolaList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
