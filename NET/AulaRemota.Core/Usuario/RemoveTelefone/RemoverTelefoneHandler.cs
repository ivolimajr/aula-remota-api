using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AulaRemota.Core.Usuario.RemoveTelefone
{
    public class RemoveTelefoneHandler : IRequestHandler<RemoveTelefoneInput, bool>
    {
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public RemoveTelefoneHandler(IRepository<TelefoneModel> telefoneRepository)
        {
            _telefoneRepository = telefoneRepository;
        }

        public async Task<bool> Handle(RemoveTelefoneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var result = _telefoneRepository.GetById(request.Id);
                if (result == null) throw new CustomException("Não Encontrado");

                _telefoneRepository.Delete(result);
                await _telefoneRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
