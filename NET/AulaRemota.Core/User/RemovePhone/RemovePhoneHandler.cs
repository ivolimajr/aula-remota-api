using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Net;
using System;

namespace AulaRemota.Core.User.RemovePhone
{
    public class RemovePhoneHandler : IRequestHandler<RemovePhoneInput, bool>
    {
        private readonly IRepository<PhoneModel, int> _phoneRepository;

        public RemovePhoneHandler(IRepository<PhoneModel, int> phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<bool> Handle(RemovePhoneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var result = _phoneRepository.Find(request.Id);
                if (result == null) throw new CustomException("Não Encontrado");

                _phoneRepository.Delete(result);
                await _phoneRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RemovePhoneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }
}
