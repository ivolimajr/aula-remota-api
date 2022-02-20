using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;
using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerHandler : IRequestHandler<CreatePartnnerInput, CreatePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel> _parceiroRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PartnnerLevelModel> _cargoRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public CreatePartnnerHandler(
            IRepository<PartnnerModel> parceiroRepository, 
            IRepository<UserModel> usuarioRepository, 
            IRepository<PartnnerLevelModel> cargoRepository,
            IRepository<PhoneModel> telefoneRepository)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<CreatePartnnerResponse> Handle(CreatePartnnerInput request, CancellationToken cancellationToken)
        {
            try
            {
                _parceiroRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new CustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _parceiroRepository.FindAsync(u => u.Cnpj == request.Cnpj);
                if (cpfResult != null) throw new CustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O Level INFORMADO EXISTE
                var Level = await _cargoRepository.GetByIdAsync(request.LevelId);
                if (Level == null) throw new CustomException("Level informado não existe");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.PhonesNumbers)
                {
                    var telefoneResult = await _telefoneRepository.FindAsync(u => u.PhoneNumber == item.PhoneNumber);
                    if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.PhoneNumber + " já em uso");
                }

                //CRIA UM USUÁRIO
                var user = new UserModel()
                {
                    Name = request.Name.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = new List<RolesModel>()
                    {
                        new RolesModel()
                        {
                            Role = Constants.Roles.PARCEIRO
                        }
                    }
                };
                user = _usuarioRepository.Create(user);
                _usuarioRepository.SaveChanges();

                //CRIA UM ENDEREÇO
                var Address = new AddressModel()
                {
                    District = request.District.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    City = request.City.ToUpper(),
                    Address = request.Address.ToUpper(),
                    Number = request.Number.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                //CRIA UM PARCEIRO
                var parceiro = new PartnnerModel()
                {
                    Name = request.Name.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Description = request.Description.ToUpper(),
                    Email = request.Email.ToUpper(),
                    PhonesNumbers = request.PhonesNumbers,
                    LevelId = request.LevelId,
                    Level = Level,
                    Address = Address,
                    User = user
                };

                var parceiroModel = await _parceiroRepository.CreateAsync(parceiro);
                _parceiroRepository.SaveChanges();

                _parceiroRepository.Commit();
                _parceiroRepository.Save();
                return new CreatePartnnerResponse
                {
                    Id = parceiroModel.Id,
                    Name = parceiroModel.Name,
                    Email = parceiroModel.Email,
                    Cnpj = parceiroModel.Cnpj,
                    Description = parceiroModel.Description,
                    PhonesNumbers = parceiroModel.PhonesNumbers,
                    LevelId = parceiroModel.LevelId,
                    UserId = parceiroModel.UserId,
                    AddressId = parceiroModel.AddressId,
                    Address = parceiroModel.Address,
                    Level = parceiroModel.Level,
                    User = parceiroModel.User
                };

            }
            catch (Exception e)
            {
                _parceiroRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _parceiroRepository.Context.Dispose();
            }
        }
    }
}
