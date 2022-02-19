using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using AulaRemota.Infra.Entity;

namespace AulaRemota.Core.User.RemovePhone
{
    public class RemovePhoneHandler : IRequestHandler<RemovePhoneInput, bool>
    {
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public RemovePhoneHandler(IRepository<PhoneModel> telefoneRepository)
        {
            _telefoneRepository = telefoneRepository;
        }

        public async Task<bool> Handle(RemovePhoneInput request, CancellationToken cancellationToken)
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
