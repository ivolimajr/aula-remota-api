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

        public EdrivingCriarHandler(IRepository<EdrivingModel> edrivingRepository, IRepository<UsuarioModel> usuarioRepository, IRepository<EdrivingCargoModel> cargoRepository)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
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

            try
            {
                EdrivingModel edrivingModel = await _edrivingRepository.CreateAsync(edriving);

                var edrivingResult = new EdrivingCriarResponse
                {
                    Id = edrivingModel.Id,
                    FullName = edrivingModel.FullName,
                    Email = edrivingModel.Email,
                    Cpf = edrivingModel.Cpf,
                    Telefone = edrivingModel.Telefone,
                    CargoId = edrivingModel.CargoId,
                    UsuarioId = edrivingModel.UsuarioId,
                    Cargo = edrivingModel.Cargo,
                    Usuario = edrivingModel.Usuario
                };
                edriving.Usuario.Password = "";
                return edrivingResult;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
