using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Entity.Parceiro.Criar
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

            //CRIA UM Parceiro
            var parceiro = new ParceiroModel();
            parceiro.Nome = request.Nome.ToUpper();
            parceiro.Cnpj = request.Cnpj.ToUpper();
            parceiro.Descricao = request.Descricao.ToUpper();
            parceiro.Email = request.Email.ToUpper();
            parceiro.Telefone = request.Telefone;
            parceiro.Cargo = cargo;
            parceiro.Endereco.Bairro = request.Bairro.ToUpper();
            parceiro.Endereco.Cep = request.Cep;
            parceiro.Endereco.Cidade = request.Cidade.ToUpper();
            parceiro.Endereco.EnderecoLogradouro = request.EnderecoLogradouro.ToUpper();
            parceiro.Endereco.Numero = request.Numero.ToUpper();
            parceiro.Endereco.Uf = request.Uf.ToUpper();
            parceiro.Usuario.Nome = request.Nome.ToUpper();
            parceiro.Usuario.Email = request.Email.ToUpper();
            parceiro.Usuario.NivelAcesso = 20;
            parceiro.Usuario.status = 1;
            parceiro.Usuario.Password = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            try
            {
                ParceiroModel result = await _parceiroRepository.CreateAsync(parceiro);

                return new ParceiroCriarResponse
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Email = result.Email,
                    Cnpj = result.Cnpj,
                    Telefone = result.Telefone,
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
