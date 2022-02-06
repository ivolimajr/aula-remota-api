using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;
using AulaRemota.Infra.Entity.Auto_Escola;

namespace AulaRemota.Core.Parceiro.Criar
{
    public class ParceiroCriarHandler : IRequestHandler<ParceiroCriarInput, ParceiroCriarResponse>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<ParceiroCargoModel> _cargoRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public ParceiroCriarHandler(
            IRepository<ParceiroModel> parceiroRepository, 
            IRepository<UsuarioModel> usuarioRepository, 
            IRepository<ParceiroCargoModel> cargoRepository,
            IRepository<TelefoneModel> telefoneRepository)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<ParceiroCriarResponse> Handle(ParceiroCriarInput request, CancellationToken cancellationToken)
        {
            try
            {
                _parceiroRepository.CreateTransaction();

                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new HttpClientCustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _parceiroRepository.FindAsync(u => u.Cnpj == request.Cnpj);
                if (cpfResult != null) throw new HttpClientCustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O CARGO INFORMADO EXISTE
                var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
                if (cargo == null) throw new HttpClientCustomException("Cargo informado não existe");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.Telefones)
                {
                    var telefoneResult = await _telefoneRepository.FindAsync(u => u.Telefone == item.Telefone);
                    if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                }

                //CRIA UM USUÁRIO
                var user = new UsuarioModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Email = request.Email.ToUpper(),
                    status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                };
                user = _usuarioRepository.Create(user);
                _usuarioRepository.SaveChanges();

                //CRIA UM ENDEREÇO
                var endereco = new EnderecoModel()
                {
                    Bairro = request.Bairro.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    Cidade = request.Cidade.ToUpper(),
                    EnderecoLogradouro = request.EnderecoLogradouro.ToUpper(),
                    Numero = request.Numero.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                //CRIA UM PARCEIRO
                var parceiro = new ParceiroModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Descricao = request.Descricao.ToUpper(),
                    Email = request.Email.ToUpper(),
                    Telefones = request.Telefones,
                    CargoId = request.CargoId,
                    Cargo = cargo,
                    Endereco = endereco,
                    Usuario = user
                };

                var parceiroModel = await _parceiroRepository.CreateAsync(parceiro);
                _parceiroRepository.SaveChanges();

                _parceiroRepository.Commit();
                _parceiroRepository.Save();
                return new ParceiroCriarResponse
                {
                    Id = parceiroModel.Id,
                    Nome = parceiroModel.Nome,
                    Email = parceiroModel.Email,
                    Cnpj = parceiroModel.Cnpj,
                    Descricao = parceiroModel.Descricao,
                    Telefones = parceiroModel.Telefones,
                    CargoId = parceiroModel.CargoId,
                    UsuarioId = parceiroModel.UsuarioId,
                    EnderecoId = parceiroModel.EnderecoId,
                    Endereco = parceiroModel.Endereco,
                    Cargo = parceiroModel.Cargo,
                    Usuario = parceiroModel.Usuario
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
