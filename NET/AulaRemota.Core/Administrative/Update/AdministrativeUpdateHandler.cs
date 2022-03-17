using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public AdministrativeUpdateHandler(
            IRepository<DrivingSchoolModel, int> drivingSchoolRepository,
            IRepository<UserModel, int> userRepository,
            IRepository<PhoneModel, int> phoneRepository,
            IRepository<EdrivingModel, int> edrivingRepository,
            IRepository<AdministrativeModel, int> administrativeRepository,
            IRepository<AddressModel, int> addressRepository
            )
        {
            _drivingSchoolRepository = drivingSchoolRepository;
            _userRepository = userRepository;
            _phoneRepository = phoneRepository;
            _edrivingRepository = edrivingRepository;
            _administrativeRepository = administrativeRepository;
            _addressRepository = addressRepository;
        }

        public async Task<AdministrativeModel> Handle(AdministrativeUpdateInput request, CancellationToken cancellationToken)
        {
            try
            {
                _administrativeRepository.CreateTransaction();
                var user = await _administrativeRepository
                    .Where(e => e.Id.Equals(request.Id))
                    .Include(e => e.PhonesNumbers)
                    .Include(e => e.Address)
                    .Include(e => e.User)
                    .FirstOrDefaultAsync();

                Check.NotNull(user, "Não encontrado");

                if (request.Birthdate >= DateTime.Today) throw new CustomException("Data da fundação inválida");
                if (request.Birthdate.Year > 1700 && request.Birthdate != user.Birthdate) user.Birthdate = request.Birthdate;

                if (string.IsNullOrEmpty(user.Email) && user.Email != request.Email)
                    if (_userRepository.Exists(e => e.Email.Equals(request.Email)))
                        throw new CustomException("Email já em uso");

                if (!string.IsNullOrEmpty(request.Cpf) && request.Cpf != user.Cpf)
                    if (_edrivingRepository.Exists(e => e.Cpf.Equals(request.Cpf)))
                        throw new CustomException("Cpf já em uso");

                if (request.PhonesNumbers != null && request.PhonesNumbers.Count > 0)
                    foreach (var item in request.PhonesNumbers)
                    {
                        if (item.Id.Equals(0))
                        {
                            user.PhonesNumbers.Add(item);
                        }
                        else
                        {
                            if (_phoneRepository.Exists(u => u.PhoneNumber == item.PhoneNumber))
                                throw new CustomException("Telefone: " + item.PhoneNumber + " já em uso");

                            var phone = user.PhonesNumbers.Where(e => e.Id.Equals(item.Id)).FirstOrDefault();
                            phone.PhoneNumber = item.PhoneNumber;
                            _phoneRepository.Update(phone);
                        }
                        await _phoneRepository.SaveChangesAsync();
                    }

                if (request.Address == null) request.Address = user.Address;


                AdministrativeModel administrative = new()
                {
                    Id = user.Id,
                    Name = request.Name ?? user.Name.ToUpper(),
                    Email = request.Email ?? user.Email,
                    Cpf = request.Cpf ?? user.Cpf.ToUpper(),
                    Identity = request.Identity ?? user.Identity.ToUpper(),
                    Origin = request.Origin ?? user.Origin.ToUpper(),
                    Birthdate = user.Birthdate,
                    DrivingSchoolId = user.DrivingSchoolId,
                    AddressId = user.AddressId,
                    Address = new()
                    {
                        Id = user.Address.Id,
                        Uf = request.Address.Uf ?? user.Address.Uf,
                        Cep = request.Address.Cep ?? user.Address.Cep,
                        Address = request.Address.Address ?? user.Address.Address,
                        AddressNumber = request.Address.AddressNumber ?? user.Address.AddressNumber,
                        Complement = request.Address.Complement ?? user.Address.Complement,
                        City = request.Address.City ?? user.Address.City,
                        District = request.Address.District ?? user.Address.District,
                    },
                    UserId = user.UserId,
                    User = new()
                    {
                        Id = user.UserId,
                        Name = request.Name ?? user.Name,
                        Email = request.Email ?? user.Email,
                        Password = user.User.Password,
                        UpdateAt = DateTime.Now
                    },
                    PhonesNumbers = user.PhonesNumbers
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
