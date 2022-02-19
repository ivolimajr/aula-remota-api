using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;
using AulaRemota.Infra.Entity.DrivingSchool;
using System.Collections.Generic;
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Core.Partnner.Create
{
    public class CreatePartnnerHandler : IRequestHandler<CreatePartnnerInput, CreatePartnnerResponse>
    {
        private readonly IRepository<PartnnerModel> _parceiroRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PartnnerLevelModel> _cargoRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public CreatePartnnerHandler(
            IRepository<PartnnerModel> parceiroRepository, 
            IRepository<UserModel> usuarioRepository, 
            IRepository<PartnnerLevelModel> cargoRepository,
            IRepository<PhoneModel> telefoneRepository)
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
                var emailResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new CustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _parceiroRepository.FindAsync(u => u.Cnpj == request.Cnpj);
                if (cpfResult != null) throw new CustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O CARGO INFORMADO EXISTE
                var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
                if (cargo == null) throw new CustomException("Cargo informado não existe");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.Telefones)
                {
                    var telefoneResult = await _telefoneRepository.FindAsync(u => u.Telefone == item.Telefone);
                    if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                }

                //CRIA UM USUÁRIO
                var user = new UserModel()
                {
                    Nome = request.Nome.ToUpper(),
                    Email = request.Email.ToUpper(),
                    status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                    Roles = new List<RolesModel>()
                    {
                        new RolesModel()
                        {
                            Role = Constants.Roles.PARCEIRO
                        }
                    }
                };
                user = _usuarioRepository.Create(user);
                _usuarioRepository.SaveChanges();

                //CRIA UM ENDEREÇO
                var endereco = new AddressModel()
                {
                    Bairro = request.Bairro.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    Cidade = request.Cidade.ToUpper(),
                    EnderecoLogradouro = request.EnderecoLogradouro.ToUpper(),
                    Numero = request.Numero.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                //CRIA UM PARCEIRO
                var parceiro = new PartnnerModel()
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
                return new CreatePartnnerResponse
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
