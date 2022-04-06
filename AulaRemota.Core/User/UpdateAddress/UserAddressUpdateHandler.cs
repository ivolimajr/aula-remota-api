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

        public UserAddressUpdateHandler(IRepository<AddressModel, int> addressRepository) => _addressRepository = addressRepository;

        public async Task<AddressModel> Handle(UserAddressUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var addressEntity = await _addressRepository.FindAsync(request.Id);
                Check.NotNull(addressEntity, "Não Encontrado");

                // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
                addressEntity.Uf = request.Uf ?? addressEntity.Uf;
                addressEntity.Cep = request.Cep ?? addressEntity.Cep;
                addressEntity.Address = request.Address ?? addressEntity.Address;
                addressEntity.District = request.District ?? addressEntity.District;
                addressEntity.City = request.City ?? addressEntity.City;
                addressEntity.AddressNumber = request.AddressNumber ?? addressEntity.AddressNumber;
                addressEntity.Complement = request.Complement ?? addressEntity.Complement;

                _addressRepository.Update(addressEntity);
                await _addressRepository.SaveChangesAsync();

                return addressEntity;
            }
            catch (Exception e)
            {
                object result = new
                {
                    addressId = request.Id,
                    addressUf = request.Uf,
                    addressCep = request.Cep,
                    addressAddress = request.Address,
                    addressDistrict = request.District,
                    addressCity = request.City,
                    addressAddressNumber = request.AddressNumber
                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(UserAddressUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = result
                });
            }
        }
    }
}
