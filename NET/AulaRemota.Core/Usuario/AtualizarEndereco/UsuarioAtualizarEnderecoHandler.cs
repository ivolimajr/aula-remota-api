using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity;
using System;

namespace AulaRemota.Core.Usuario.AtualizarEndereco
{
    public class UsuarioAtualizarEnderecoHandler : IRequestHandler<UsuarioAtualizarEnderecoInput, EnderecoModel>
    {
        private readonly IRepository<EnderecoModel> _enderecoRepository;

        public UsuarioAtualizarEnderecoHandler(IRepository<EnderecoModel> enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<EnderecoModel> Handle(UsuarioAtualizarEnderecoInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var entity = await _enderecoRepository.GetByIdAsync(request.Id);
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

                // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
                if (request.Uf != null) entity.Uf = request.Uf.ToUpper();
                if (request.Cep != null) entity.Cep = request.Cep.ToUpper();
                if (request.EnderecoLogradouro != null) entity.EnderecoLogradouro = request.EnderecoLogradouro.ToUpper();
                if (request.Bairro != null) entity.Bairro = request.Bairro.ToUpper();
                if (request.Cidade != null) entity.Cidade = request.Cidade.ToUpper();
                if (request.Numero != null) entity.Numero = request.Numero.ToUpper();

                _enderecoRepository.Update(entity);
                await _enderecoRepository.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
