using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.Criar
{
    public class EdrivingCriarHandler : IRequestHandler<EdrivingCriarInput, EdrivingCriarResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<EdrivingCargoModel> _cargoRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;
        private readonly IMediator _mediator;

        public EdrivingCriarHandler(
            IRepository<EdrivingModel> edrivingRepository,
            IRepository<UsuarioModel> usuarioRepository,
            IRepository<EdrivingCargoModel> cargoRepository,
            IRepository<TelefoneModel> telefoneRepository,
            IMediator mediator
            )
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
            _mediator = mediator;
        }

        public async Task<EdrivingCriarResponse> Handle(EdrivingCriarInput request, CancellationToken cancellationToken)
        {
            try
            {
                _edrivingRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new HttpClientCustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _edrivingRepository.FindAsync(u => u.Cpf == request.Cpf);
                if (cpfResult != null) throw new HttpClientCustomException("Cpf já existe em nossa base de dados");

                //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                if(request.Telefones != null)
                {
                    foreach (var item in request.Telefones)
                    {
                        var telefoneResult = await _telefoneRepository.FindAsync(u => u.Telefone == item.Telefone);
                        if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                    }
                }
             

                //VERIFICA SE O CARGO INFORMADO EXISTE
                var cargo = _cargoRepository.GetById(request.CargoId);
                if (cargo == null) throw new HttpClientCustomException("Cargo informado não existe");

                //CRIA UM USUÁRIO
                var user = new UsuarioModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Email = request.Email.ToUpper(),
                    NivelAcesso = 10,
                    status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                };

                //CRIA UM EDRIVING
                var edriving = new EdrivingModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Cpf = request.Cpf.ToUpper(),
                    Email = request.Email.ToUpper(),
                    CargoId = request.CargoId,
                    Telefones = request.Telefones,
                    Cargo = cargo,
                    Usuario = user
                };
                var edrivingModel = await _edrivingRepository.CreateAsync(edriving);

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _edrivingRepository.Commit();
                _edrivingRepository.Save();

                return new EdrivingCriarResponse()
                {
                    Id = edrivingModel.Id,
                    Nome = edrivingModel.Nome,
                    Email = edrivingModel.Email,
                    Cpf = edrivingModel.Cpf,
                    Telefones = edrivingModel.Telefones,
                    CargoId = edrivingModel.CargoId,
                    UsuarioId = edrivingModel.UsuarioId,
                    Cargo = edrivingModel.Cargo,
                    Usuario = edrivingModel.Usuario,
                };
            }
            catch (Exception e)
            {
                _edrivingRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _edrivingRepository.Context.Dispose();
            }
        }
    }
}