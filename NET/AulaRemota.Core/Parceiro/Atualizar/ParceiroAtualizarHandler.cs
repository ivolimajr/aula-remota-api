using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Parceiro.Atualizar
{
    public class ParceiroAtualizarHandler : IRequestHandler<ParceiroAtualizarInput, ParceiroAtualizarResponse>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<ParceiroCargoModel> _cargoRepository;
        private readonly IRepository<EnderecoModel> _enderecoRepository;

        public ParceiroAtualizarHandler(IRepository<ParceiroModel> parceiroRepository,
                IRepository<UsuarioModel> usuarioRepository,
                IRepository<ParceiroCargoModel> cargoRepository,
                IRepository<EnderecoModel> enderecoRepository
            )
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _enderecoRepository = enderecoRepository;
        }


        public async Task<ParceiroAtualizarResponse> Handle(ParceiroAtualizarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            //BUSCA O OBJETO A SER ATUALIZADO
            var entity = _parceiroRepository.GetById(request.Id);
            if (entity == null) throw new HttpClientCustomException("Não Encontrado");

            //BUSCA O OBJETO ENDEREÇO A SER ATUALIZADO
            var endereco = _enderecoRepository.GetById(entity.EnderecoId);
            if (endereco == null) throw new HttpClientCustomException("Errro ao carregar endereço");

            entity.Endereco = endereco;

            //SE FOR INFORMADO UM NOVO CARGO, O CARGO ATUAL SERÁ ATUALIZADO
            if (request.CargoId != 0)
            {
                var cargo = _cargoRepository.GetById(request.CargoId);
                if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

                //SE O CARGO EXISTE, O OBJETO SERÁ ATUALIZADO
                entity.CargoId = cargo.Id;
                entity.Cargo = cargo;
            }
            else
            {
                //SE O USUÁRIO NÃO INFORMAR UM CARGO, É SETADO O CARGO ANTERIOR
                var cargo = _cargoRepository.GetById(entity.CargoId);
                if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

                entity.Cargo = cargo;
            }


            //BUSCA O OBJETO USUARIO PARA ATUALIZAR
            var usuario = _usuarioRepository.GetById(entity.UsuarioId);
            if (usuario == null) throw new HttpClientCustomException("Errro ao carregar usuário");

            //ATUALIZA O NOME E EMAIL
            if (request.Nome != null) usuario.Nome = request.Nome.ToUpper();
            if (request.Email != null) usuario.Email = request.Email.ToUpper();


            // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
            if (request.Nome != null)   entity.Nome     = request.Nome.ToUpper();
            if (request.Email != null)      entity.Email        = request.Email.ToUpper();
            if (request.Telefone != null)   entity.Telefones     = new List<TelefoneModel> { new TelefoneModel { Telefone = request.Telefone } };
            if (request.Cnpj != null)       entity.Cnpj         = request.Cnpj.ToUpper();
            if (request.Descricao != null)  entity.Descricao    = request.Descricao.ToUpper();

            // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
            if (request.Uf != null)                     entity.Endereco.Uf                  = request.Uf.ToUpper();
            if (request.Cep != null)                    entity.Endereco.Cep                 = request.Cep.ToUpper();
            if (request.EnderecoLogradouro != null)     entity.Endereco.EnderecoLogradouro  = request.EnderecoLogradouro.ToUpper();
            if (request.Bairro != null)                 entity.Endereco.Bairro              = request.Bairro.ToUpper();
            if (request.Cidade != null)                 entity.Endereco.Cidade              = request.Cidade.ToUpper();
            if (request.Numero != null)                 entity.Endereco.Numero              = request.Numero.ToUpper();

            try
            {
                ParceiroModel parceiroModel = _parceiroRepository.Update(entity);
                if (parceiroModel == null) throw new HttpClientCustomException("Errro ao salvar dados usuário");

                return new ParceiroAtualizarResponse
                {
                    Id = parceiroModel.Id,
                    Nome = parceiroModel.Nome,
                    Email = parceiroModel.Email,
                    Cnpj = parceiroModel.Cnpj,
                    Telefones = parceiroModel.Telefones,
                    CargoId = parceiroModel.CargoId,
                    UsuarioId = parceiroModel.UsuarioId,
                    Cargo = parceiroModel.Cargo,
                    Usuario = parceiroModel.Usuario,
                    EnderecoId = parceiroModel.EnderecoId,
                    Endereco = parceiroModel.Endereco
                };

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
