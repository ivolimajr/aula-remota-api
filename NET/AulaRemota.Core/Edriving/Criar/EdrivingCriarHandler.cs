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

            //CRIA UM EDRIVING
            var edriving = new EdrivingModel();

            edriving.Nome = request.Nome.ToUpper();
            edriving.Cpf = request.Cpf.ToUpper();
            edriving.Email = request.Email.ToUpper();
            edriving.Telefone = request.Telefone.ToUpper();
            edriving.Cargo = cargo;
            edriving.Usuario.Email = request.Email.ToUpper();
            edriving.Usuario.Nome = request.Nome.ToUpper();
            edriving.Usuario.NivelAcesso = 10;
            edriving.Usuario.status = request.Status;
            edriving.Usuario.Password = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            EdrivingCriarResponse edrivingResult = new EdrivingCriarResponse();
            try
            {
                EdrivingModel edrivingModel = await _edrivingRepository.CreateAsync(edriving);

                edrivingResult.Id = edrivingModel.Id;
                edrivingResult.Nome = edrivingModel.Nome;
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
                emailResult = null;
                cargo = null;
                edriving = null;
                edrivingResult = null;
            }
        }
    }
}
