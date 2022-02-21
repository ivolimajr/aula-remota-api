using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;
using AulaRemota.Core.Services;
using System.Net;

namespace AulaRemota.Core.Edriving.Create
{
    public class EdrivingCreateHandler : IRequestHandler<EdrivingCreateInput, EdrivingCreateResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<EdrivingLevelModel> _cargoRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;
        private readonly IValidatorServices _validator;
        private readonly IMediator _mediator;

        public EdrivingCreateHandler(
            IRepository<EdrivingModel> edrivingRepository,
            IRepository<UserModel> usuarioRepository,
            IRepository<EdrivingLevelModel> cargoRepository,
            IRepository<PhoneModel> telefoneRepository,
            IValidatorServices validator,
            IMediator mediator
            )
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<EdrivingCreateResponse> Handle(EdrivingCreateInput request, CancellationToken cancellationToken)
        {
            try
            {
                _edrivingRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new CustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _edrivingRepository.FindAsync(u => u.Cpf == request.Cpf);
                if (cpfResult != null) throw new CustomException("Cpf já existe em nossa base de dados");

                //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                if(request.PhonesNumbers != null)
                {
                    foreach (var item in request.PhonesNumbers)
                    {
                        var telefoneResult = await _telefoneRepository.FindAsync(u => u.PhoneNumber == item.PhoneNumber);
                        if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.PhoneNumber + " já em uso");
                    }
                }

                //VERIFICA SE O Level INFORMADO EXISTE
                var Level = _cargoRepository.GetById(request.LevelId);
                if (Level == null) throw new CustomException("Level informado não existe");

                //CRIA UM USUÁRIO
                var user = new UserModel()
                {
                    Name = request.Name.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = new List<RolesModel>()
                    {
                        new RolesModel(){
                        Role = Constants.Roles.EDRIVING
                        }
                    }
                };

                //CRIA UM EDRIVING
                var edriving = new EdrivingModel()
                {
                    Name = request.Name.ToUpper(),
                    Cpf = request.Cpf.ToUpper(),
                    Email = request.Email.ToUpper(),
                    LevelId = request.LevelId,
                    PhonesNumbers = request.PhonesNumbers,
                    Level = Level,
                    User = user
                };
                var edrivingModel = await _edrivingRepository.CreateAsync(edriving);

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _edrivingRepository.Commit();
                _edrivingRepository.Save();

                return new EdrivingCreateResponse()
                {
                    Id = edrivingModel.Id,
                    Name = edrivingModel.Name,
                    Email = edrivingModel.Email,
                    Cpf = edrivingModel.Cpf,
                    PhonesNumbers = edrivingModel.PhonesNumbers,
                    LevelId = edrivingModel.LevelId,
                    UserId = edrivingModel.UserId,
                    Level = edrivingModel.Level,
                    User = edrivingModel.User,
                };
            }
            catch (CustomException e)
            {
                _edrivingRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingCreateResponse),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            finally
            {
                _edrivingRepository.Context.Dispose();
            }
        }
    }
}