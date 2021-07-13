using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public class EnderecoServices : IEnderecoServices
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoServices(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        Endereco IEnderecoServices.Create(Endereco entity)
        {
            entity.Bairro = entity.Bairro.ToUpper();
            entity.Cidade = entity.Cidade.ToUpper();
            entity.EnderecoLogradouro = entity.EnderecoLogradouro.ToUpper();
            entity.Numero = entity.Numero.ToUpper();
            entity.Uf = entity.Uf.ToUpper();

            return _enderecoRepository.Create(entity);
        }

        void IEnderecoServices.Delete(int id)
        {
            var result = _enderecoRepository.GetById(id);
            _enderecoRepository.Delete(result);
        }

        IEnumerable<Endereco> IEnderecoServices.GetAll()
        {
            return _enderecoRepository.GetAll();
        }

        Endereco IEnderecoServices.GetById(int id)
        {
            return _enderecoRepository.GetById(id);
        }

        IEnumerable<Endereco> IEnderecoServices.GetWhere(Func<Endereco, bool> predicado)
        {
            return _enderecoRepository.GetWhere(predicado);
        }

        Endereco IEnderecoServices.Update(Endereco entity)
        {
            entity.Bairro = entity.Bairro.ToUpper();
            entity.Cidade = entity.Cidade.ToUpper();
            entity.EnderecoLogradouro = entity.EnderecoLogradouro.ToUpper();
            entity.Numero = entity.Numero.ToUpper();
            entity.Uf = entity.Uf.ToUpper();
            return _enderecoRepository.Update(entity);
        }
    }
}
