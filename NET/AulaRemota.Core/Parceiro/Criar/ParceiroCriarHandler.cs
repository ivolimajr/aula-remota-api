using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;

namespace AulaRemota.Core.Parceiro.Criar
{
    public  class ParceiroCriarHandler : IRequestHandler<ParceiroCriarInput, ParceiroCriarResponse>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<ParceiroCargoModel> _cargoRepository;

        public ParceiroCriarHandler(IRepository<ParceiroModel> parceiroRepository, IRepository<UsuarioModel> usuarioRepository, IRepository<ParceiroCargoModel> cargoRepository)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
        }

        public async Task<ParceiroCriarResponse> Handle(ParceiroCriarInput request, CancellationToken cancellationToken)
        {
            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            var emailResult = _usuarioRepository.Find(u => u.Email == request.Email);
            if (emailResult != null) throw new HttpClientCustomException("Email em uso");

            //VERIFICA SE O CARGO INFORMADO EXISTE
            var cargo = _cargoRepository.GetById(request.CargoId);
            if (cargo == null) throw new HttpClientCustomException("Cargo informado não existe");
            var user = new UsuarioModel();

            user.Nome = request.Nome.ToUpper();
            user.Email = request.Email.ToUpper();
            user.NivelAcesso = 20;
            user.status = 1;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            //CRIA UM ENDEREÇO
            var endereco = new EnderecoModel();

            endereco.Bairro = request.Bairro.ToUpper();
            endereco.Cep = request.Cep.ToUpper();
            endereco.Cidade = request.Cidade.ToUpper();
            endereco.EnderecoLogradouro = request.EnderecoLogradouro.ToUpper();
            endereco.Numero = request.Numero.ToUpper();
            endereco.Uf = request.Uf.ToUpper();

            //CRIA UM Parceiro
            var parceiro = new ParceiroModel()
            {
                Nome = request.Nome.ToUpper(),
                Cnpj = request.Cnpj.ToUpper(),
                Descricao = request.Descricao.ToUpper(),
                Email = request.Email.ToUpper(),
                Telefones = request.Telefones.ToList(),
                CargoId = request.CargoId,
                Cargo = cargo,
                Endereco = endereco,
                Usuario = user
            };

            try
            {
                ParceiroModel result = await _parceiroRepository.CreateAsync(parceiro);

                return new ParceiroCriarResponse
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    Cnpj = result.Cnpj,
                    Telefones = result.Telefones,
                    CargoId = result.CargoId,
                    UsuarioId = result.UsuarioId,
                    EnderecoId = result.EnderecoId,
                    Endereco = result.Endereco,
                    Cargo = result.Cargo,
                    Usuario = result.Usuario,
                    Descricao = result.Descricao
                };

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                emailResult = null;
                parceiro = null;
                cargo = null;
            }
        }
    }
}
