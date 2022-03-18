using AulaRemota.Core.Services.Phone.UpdatePhone;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.Update
{
    internal class AdministrativeUpdateHandler : IRequestHandler<AdministrativeUpdateInput, AdministrativeModel>
    {
        private readonly IRepository<DrivingSchoolModel, int> _drivingSchoolRepository;
        private readonly IRepository<UserModel, int> _userRepository;
        private readonly IRepository<PhoneModel, int> _phoneRepository;
        private readonly IRepository<EdrivingModel, int> _edrivingRepository;
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;
        private readonly IRepository<AddressModel, int> _addressRepository;
        private readonly IMediator _mediator;

        public AdministrativeUpdateHandler(
            IRepository<DrivingSchoolModel, int> drivingSchoolRepository,
            IRepository<UserModel, int> userRepository,
            IRepository<PhoneModel, int> phoneRepository,
            IRepository<EdrivingModel, int> edrivingRepository,
            IRepository<AdministrativeModel, int> administrativeRepository,
            IRepository<AddressModel, int> addressRepository, 
            IMediator mediator
            )
        {
            _drivingSchoolRepository = drivingSchoolRepository;
            _userRepository = userRepository;
            _phoneRepository = phoneRepository;
            _edrivingRepository = edrivingRepository;
            _administrativeRepository = administrativeRepository;
            _addressRepository = addressRepository;
            _mediator = mediator;
        }

        public async Task<AdministrativeModel> Handle(AdministrativeUpdateInput request, CancellationToken cancellationToken)
        {
            try
            {
                _administrativeRepository.CreateTransaction();
                var administrativeEntity = await _administrativeRepository
                    .Where(e => e.Id.Equals(request.Id))
                    .Include(e => e.PhonesNumbers)
                    .Include(e => e.Address)
                    .Include(e => e.User)
                    .FirstOrDefaultAsync();

                Check.NotNull(administrativeEntity, "Não encontrado");

                if (request.Birthdate >= DateTime.Today) throw new CustomException("Data da fundação inválida");
                if (request.Birthdate.Year > 1700 && request.Birthdate != administrativeEntity.Birthdate) administrativeEntity.Birthdate = request.Birthdate;

                if (string.IsNullOrEmpty(request.Email) && request.Email != administrativeEntity.Email)
                    if (_userRepository.Exists(e => e.Email.Equals(request.Email)))
                        throw new CustomException("Email já em uso");

                if (!string.IsNullOrEmpty(request.Cpf) && request.Cpf != administrativeEntity.Cpf)
                    if (_edrivingRepository.Exists(e => e.Cpf.Equals(request.Cpf)))
                        throw new CustomException("Cpf já em uso");

                if (Check.NotNull(request.PhonesNumbers))
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            administrativeEntity.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            var res = await _mediator.Send(new PhoneUpdateInput
                            {
                                CurrentPhoneList = administrativeEntity.PhonesNumbers,
                                RequestPhoneList = request.PhonesNumbers
                            });
                            if (!res) throw new CustomException("Falha ao atualizar contato");
                        }
                    }

                if (request.Address == null) request.Address = administrativeEntity.Address;


                AdministrativeModel administrative = new()
                {
                    Id = administrativeEntity.Id,
                    Name = request.Name ?? administrativeEntity.Name.ToUpper(),
                    Email = request.Email ?? administrativeEntity.Email,
                    Cpf = request.Cpf ?? administrativeEntity.Cpf.ToUpper(),
                    Identity = request.Identity ?? administrativeEntity.Identity.ToUpper(),
                    Origin = request.Origin ?? administrativeEntity.Origin.ToUpper(),
                    Birthdate = administrativeEntity.Birthdate,
                    DrivingSchoolId = administrativeEntity.DrivingSchoolId,
                    AddressId = administrativeEntity.AddressId,
                    Address = new()
                    {
                        Id = administrativeEntity.AddressId,
                        Uf = request.Address.Uf ?? administrativeEntity.Address.Uf,
                        Cep = request.Address.Cep ?? administrativeEntity.Address.Cep,
                        Address = request.Address.Address ?? administrativeEntity.Address.Address,
                        AddressNumber = request.Address.AddressNumber ?? administrativeEntity.Address.AddressNumber,
                        Complement = request.Address.Complement ?? administrativeEntity.Address.Complement,
                        City = request.Address.City ?? administrativeEntity.Address.City,
                        District = request.Address.District ?? administrativeEntity.Address.District,
                    },
                    UserId = administrativeEntity.UserId,
                    User = new()
                    {
                        Id = administrativeEntity.UserId,
                        Name = request.Name ?? administrativeEntity.Name,
                        Email = request.Email ?? administrativeEntity.Email,
                        Password = administrativeEntity.User.Password,
                        UpdateAt = DateTime.Now
                    },
                    PhonesNumbers = administrativeEntity.PhonesNumbers
                };

                _userRepository.Update(administrative.User);
                _userRepository.SaveChanges();

                _addressRepository.Update(administrative.Address);
                _addressRepository.SaveChanges();


                _administrativeRepository.Update(administrative);
                _administrativeRepository.SaveChanges();

                _administrativeRepository.Commit();

                return administrative;
            }
            catch (Exception e)
            {
                object result = new
                {
                    userId = request.Id,
                    userName = request.Email ?? "",
                    userEmail = request.Email ?? "",

                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(AdministrativeUpdateInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = result
                });
            }
        }
    }
}
