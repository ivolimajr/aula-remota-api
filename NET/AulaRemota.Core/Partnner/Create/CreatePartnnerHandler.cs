using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerHandler : IRequestHandler<CreatePartnnerInput, CreatePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel, int> _parceiroRepository;
        private readonly IRepository<UserModel, int>_usuarioRepository;
        private readonly IRepository<PartnnerLevelModel, int>_cargoRepository;
        private readonly IRepository<PhoneModel, int> _telefoneRepository;

        public CreatePartnnerHandler(
            IRepository<PartnnerModel, int> parceiroRepository, 
            IRepository<UserModel, int>usuarioRepository, 
            IRepository<PartnnerLevelModel, int>cargoRepository,
            IRepository<PhoneModel, int> telefoneRepository)
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
                var emailResult = await _usuarioRepository.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new CustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _parceiroRepository.FirstOrDefaultAsync(u => u.Cnpj == request.Cnpj);
                if (cpfResult != null) throw new CustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O Level INFORMADO EXISTE
                var Level = await _cargoRepository.FindAsync(request.LevelId);
                if (Level == null) throw new CustomException("Level informado não existe", HttpStatusCode.NotFound);

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.PhonesNumbers)
                {
                    var telefoneResult = await _telefoneRepository.FirstOrDefaultAsync(u => u.PhoneNumber == item.PhoneNumber);
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
                user = _usuarioRepository.Add(user);
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

                var parceiroModel = await _parceiroRepository.AddAsync(parceiro);
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
            catch (CustomException e)
            {
                _parceiroRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(CreatePartnnerInput),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
            finally
            {
                _parceiroRepository.Context.Dispose();
            }
        }
    }
}
