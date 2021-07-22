using AulaRemota.Core.Email.EnviarEmailRegistro;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Entity.Edriving.Criar
{
    public  class EdrivingCriarHandler : IRequestHandler<EdrivingCriarInput, EdrivingCriarResponse>
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

            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            var emailResult = _usuarioRepository.Find(u => u.Email == request.Email);
            if (emailResult != null) throw new HttpClientCustomException("Email em uso");

            //VERIFICA SE O CARGO INFORMADO EXISTE
            var cargo = _cargoRepository.GetById(request.CargoId);
            if (cargo == null) throw new HttpClientCustomException("Cargo informado não existe");

            //CRIA USUARIO
            var user = new UsuarioModel();

            user.FullName = request.FullName.ToUpper();
            user.Email = request.Email.ToUpper();
            user.NivelAcesso = 10;
            user.status = request.Status;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            //CRIA UM EDRIVING
            var edriving = new EdrivingModel()
            {
                FullName = request.FullName.ToUpper(),
                Cpf = request.Cpf.ToUpper(),
                Email = request.Email.ToUpper(),
                CargoId = request.CargoId,
                Telefone = request.Telefone.ToUpper(),
                Cargo = cargo,
                Usuario = user,
                UsuarioId = user.Id
            };
            EdrivingCriarResponse edrivingResult = new EdrivingCriarResponse();
            try
            {
                EdrivingModel edrivingModel = await _edrivingRepository.CreateAsync(edriving);

                edrivingResult.Id = edrivingModel.Id;
                edrivingResult.FullName = edrivingModel.FullName;
                edrivingResult.Email = edrivingModel.Email;
                edrivingResult.Cpf = edrivingModel.Cpf;
                edrivingResult.Telefone = edrivingModel.Telefone;
                edrivingResult.CargoId = edrivingModel.CargoId;
                edrivingResult.UsuarioId = edrivingModel.UsuarioId;
                edrivingResult.Cargo = edrivingModel.Cargo;
                edrivingResult.Usuario = edrivingModel.Usuario;
                edriving.Usuario.Password = "";

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                return edrivingResult;
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                user = null;
                edriving = null;
                edrivingResult = null;
            }
        }
    }
}
