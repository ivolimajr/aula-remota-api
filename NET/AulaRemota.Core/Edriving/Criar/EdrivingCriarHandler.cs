using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.Criar
{
    public class EdrivingCriarHandler : IRequestHandler<EdrivingCriarInput, EdrivingCriarResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<EdrivingCargoModel> _cargoRepository;
        private readonly IMediator _mediator;

        public EdrivingCriarHandler(
            IRepository<EdrivingModel> edrivingRepository,
            IRepository<UsuarioModel> usuarioRepository,
            IRepository<EdrivingCargoModel> cargoRepository,
            IMediator mediator
            )
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _mediator = mediator;
        }

        public async Task<EdrivingCriarResponse> Handle(EdrivingCriarInput request, CancellationToken cancellationToken)
        {

            try
            {
                _edrivingRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailResult = _usuarioRepository.Find(u => u.Email == request.Email);
                if (emailResult != null) throw new HttpClientCustomException("Email em uso");

                //VERIFICA SE O CARGO INFORMADO EXISTE
                var cargo = _cargoRepository.GetById(request.CargoId);
                if (cargo == null) throw new HttpClientCustomException("Cargo informado não existe");

                //CRIA UM USUÁRIO
                var user = new UsuarioModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Email = request.Email.ToUpper(),
                    NivelAcesso = 10,
                    status = request.Status,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                };
                user = _usuarioRepository.Create(user);
                _usuarioRepository.SaveChanges();

                //CRIA UM EDRIVING
                var edriving = new EdrivingModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Cpf = request.Cpf.ToUpper(),
                    Email = request.Email.ToUpper(),
                    CargoId = request.CargoId,
                    Telefones = request.Telefones.ToList(),
                    Cargo = cargo,
                    Usuario = user
                };
                var edrivingModel = await _edrivingRepository.CreateAsync(edriving);
                _edrivingRepository.SaveChanges();


                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _edrivingRepository.Commit();
                _edrivingRepository.Save();

                return new EdrivingCriarResponse()
                {
                    Id = edrivingModel.Id,
                    Nome = edrivingModel.Nome,
                    Email = edrivingModel.Email,
                    Cpf = edrivingModel.Cpf,
                    Telefones = edrivingModel.Telefones.ToList(),
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