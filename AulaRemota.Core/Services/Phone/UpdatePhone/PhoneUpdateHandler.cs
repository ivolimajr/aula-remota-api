using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Services.Phone.UpdatePhone
{
    public class PhoneUpdateHandler : IRequestHandler<PhoneUpdateInput, bool>
    {
        private readonly IRepository<PhoneModel, int> _authUserRepository;

        public PhoneUpdateHandler(IRepository<PhoneModel, int> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<bool> Handle(PhoneUpdateInput request, CancellationToken cancellationToken)
        {
            if (!Check.NotNull(request.RequestPhoneList)) return false;

            foreach (var requestItem in request.RequestPhoneList)
            {
                if (requestItem.Id > 0)
                {
                    //SE O NÚMERO DE TELEFONE EXISTE E NÃO PERTENCE AO USUÁRIO ATUAL: SIGNIFICA QUE JÁ ESTÁ EM USO É PERTENCE A OUTRO USUÁRIO
                    if (_authUserRepository.Exists(u => u.PhoneNumber == requestItem.PhoneNumber))
                        if (!request.CurrentPhoneList.Any(e => e.PhoneNumber == requestItem.PhoneNumber))
                            throw new CustomException("Telefone: " + requestItem.PhoneNumber + " já em uso");

                    var phoneEntity = request.CurrentPhoneList.Where(e => e.Id.Equals(requestItem.Id)).FirstOrDefault();
                    if (phoneEntity.PhoneNumber != requestItem.PhoneNumber)
                    {
                        phoneEntity.PhoneNumber = requestItem.PhoneNumber;
                        _authUserRepository.Update(phoneEntity);
                    }
                }
            }
            await _authUserRepository.SaveChangesAsync();
            return true;
        }
    }
}
