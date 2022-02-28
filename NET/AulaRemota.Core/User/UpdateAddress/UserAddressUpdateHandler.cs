using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Net;
using System;

namespace AulaRemota.Core.User.UpdateAddress
{
    public class UserAddressUpdateHandler : IRequestHandler<UserAddressUpdateInput, AddressModel>
    {
        private readonly IRepository<AddressModel, int> _addressRepository;

        public UserAddressUpdateHandler(IRepository<AddressModel, int> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressModel> Handle(UserAddressUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var entity = await _addressRepository.FindAsync(request.Id);
                if (entity == null) throw new CustomException("Não Encontrado");

                // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
                if (request.Uf != null) entity.Uf = request.Uf.ToUpper();
                if (request.Cep != null) entity.Cep = request.Cep.ToUpper();
                if (request.Address != null) entity.Address = request.Address.ToUpper();
                if (request.District != null) entity.District = request.District.ToUpper();
                if (request.City != null) entity.City = request.City.ToUpper();
                if (request.Number != null) entity.Number = request.Number.ToUpper();

                _addressRepository.Update(entity);
                await _addressRepository.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UserAddressUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound
                });
            }
        }
    }
}
