using AulaRemota.Api.Models.Requests;
using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Services
{
    public class ParceiroServices : IParceiroServices
    {
        private readonly IParceiroRepository _parceiroRepository;
        private readonly IParceiroCargoServices _parceiroCargoServices;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IEnderecoServices _enderecoServices;

        public ParceiroServices(IParceiroRepository parceiroRepository, IUsuarioServices usuarioServices, IParceiroCargoServices parceiroCargoServices, IEnderecoServices enderecoServices)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioServices = usuarioServices;
            _parceiroCargoServices = parceiroCargoServices;
            _enderecoServices = enderecoServices;
        }

        Parceiro IParceiroServices.Create(ParceiroCreateRequest entity)
        {
            //VERIFICA SE O CARGO INFORMADO EXISTE
            var cargo = _parceiroCargoServices.GetById(entity.CargoId);
            if (cargo == null) return null;

            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            var emailResult = _usuarioServices.GetByEmail(entity.Email);
            if (emailResult != null) return null;

            //CRIA USUARIO
            var user = new Usuario();

            user.FullName = entity.FullName.ToUpper();
            user.Email = entity.Email.ToUpper();
            user.NivelAcesso = 20;
            user.status = 1;
            user.Password =  BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //CRIA UM ENDEREÇO
            var endereco = new Endereco();

            endereco.Bairro = entity.Bairro.ToUpper();
            endereco.Cep = entity.Cep.ToUpper();
            endereco.Cidade = entity.Cidade.ToUpper();
            endereco.EnderecoLogradouro = entity.EnderecoLogradouro.ToUpper();
            endereco.Numero = entity.Numero.ToUpper();
            endereco.Uf = entity.Uf.ToUpper();

            //CRIA UM Parceiro
            var Parceiro = new Parceiro()
            {
                FullName = entity.FullName.ToUpper(),
                Cnpj = entity.Cnpj.ToUpper(),
                Descricao = entity.Descricao.ToUpper(),
                Email = entity.Email.ToUpper(),
                Telefone = entity.Telefone,
                CargoId = entity.CargoId,
                Cargo = cargo,
                Endereco = endereco,
                Usuario = user
            };

            //CRIA A ENTIDADE
            return _parceiroRepository.Create(Parceiro);
        }

        IEnumerable<Parceiro> IParceiroServices.GetAll()
        {
            return _parceiroRepository.GetAll();
        }

        IEnumerable<Parceiro> IParceiroServices.GetAllWithRelationship()
        {
            return _parceiroRepository.GetAllWithRelationship();
        }

        Parceiro IParceiroServices.GetById(int id)
        {
            return _parceiroRepository.GetById(id);
        }

        IEnumerable<Parceiro> IParceiroServices.GetWhere(Expression<Func<Parceiro, bool>> predicado)
        {
            return _parceiroRepository.GetWhere(predicado);
        }

        Parceiro IParceiroServices.Update(ParceiroCreateRequest parceiro)
        {
            var entity = _parceiroRepository.GetById(parceiro.Id);
            if (entity == null) return null;

            var usuario = _usuarioServices.GetById(entity.UsuarioId);
            if (usuario == null) return null;

            usuario.FullName = parceiro.FullName.ToUpper();
            usuario.Email = parceiro.Email.ToUpper();

            var resultUsuario = _usuarioServices.Update(usuario);
            if (resultUsuario == null) return null;

            var endereco = _enderecoServices.GetById(entity.EnderecoId);
            if (endereco == null) return null;

            endereco.Bairro = parceiro.Bairro.ToUpper();
            endereco.Cep = parceiro.Cep.ToUpper();
            endereco.Cidade = parceiro.Cidade.ToUpper();
            endereco.EnderecoLogradouro = parceiro.EnderecoLogradouro.ToUpper();
            endereco.Numero = parceiro.Numero.ToUpper();
            endereco.Uf = parceiro.Uf.ToUpper();

            var resultEndereco = _enderecoServices.Update(endereco);
            if (resultEndereco == null) return null;

            entity.FullName = parceiro.FullName.ToUpper();
            entity.Email = parceiro.Email.ToUpper();
            entity.Cnpj = parceiro.Cnpj.ToUpper();
            entity.Descricao = parceiro.Descricao.ToUpper();
            entity.Telefone = parceiro.Telefone;
            entity.CargoId = parceiro.CargoId;

            return _parceiroRepository.Update(entity);
        }

        bool IParceiroServices.Delete(int id)
        {
            var Parceiro = _parceiroRepository.GetById(id);
            if (Parceiro == null) return false;

            var usuario = _usuarioServices.GetById(Parceiro.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 0;

            if (_usuarioServices.Update(usuario) != null) return true;
            return false;
        }

        bool IParceiroServices.Ativar(int id)
        {
            var Parceiro = _parceiroRepository.GetById(id);
            if (Parceiro == null) return false;

            var usuario = _usuarioServices.GetById(Parceiro.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 1;

            if (_usuarioServices.Update(usuario) != null) return true;
            return false;
        }

        bool IParceiroServices.Inativar(int id)
        {
            var Parceiro = _parceiroRepository.GetById(id);
            if (Parceiro == null) return false;

            var usuario = _usuarioServices.GetById(Parceiro.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 2;

            if (_usuarioServices.Update(usuario) != null) return true;
            return false;
        }

        bool IParceiroServices.ValidateEntity(ParceiroCreateRequest entity)
        {
            if( entity.Cnpj      == null ||
                entity.FullName == null ||
                entity.Email    == null ||
                entity.Cnpj == null ||
                entity.Descricao == null ||
                entity.Telefone    == null ||
                entity.Bairro == null ||
                entity.Cep == null ||
                entity.Cidade == null ||
                entity.EnderecoLogradouro == null ||
                entity.Numero == null ||
                entity.Uf == null ||
                entity.CargoId  == 0)
                return false;

            return true;
        }
    }
}
