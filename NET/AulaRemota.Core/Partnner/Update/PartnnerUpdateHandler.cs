using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AulaRemota.Core.Partnner.Update
{
    public class PartnnerUpdateHandler : IRequestHandler<PartnnerUpdateInput, PartnnerUpdateResponse>
    {
        private readonly IRepository<PartnnerModel> _parceiroRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PartnnerLevelModel> _cargoRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public PartnnerUpdateHandler(
                IRepository<PartnnerModel> parceiroRepository,
                IRepository<UserModel> usuarioRepository,
                IRepository<PartnnerLevelModel> cargoRepository,
                IRepository<PhoneModel> telefoneRepository
         )
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<PartnnerUpdateResponse> Handle(PartnnerUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                _parceiroRepository.CreateTransaction();
                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = await _parceiroRepository.Context
                        .Set<PartnnerModel>()
                        .Include(e => e.Cargo)
                        .Include(e => e.Usuario)
                        .Include(e => e.Endereco)
                        .Include(e => e.Telefones)
                        .Where(e => e.Id == request.Id)
                        .FirstOrDefaultAsync();

                if (entity == null) throw new CustomException("Não Encontrado");

                //SE FOR INFORMADO UM NOVO CARGO, O CARGO ATUAL SERÁ ATUALIZADO
                if (request.CargoId > 0 && request.CargoId != entity.CargoId)
                {
                    //VERIFICA SE O CARGO INFORMADO EXISTE
                    var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
                    if (cargo == null) throw new CustomException("Cargo Não Encontrado");

                    //SE O CARGO EXISTE, O OBJETO SERÁ ATUALIZADO
                    entity.CargoId = cargo.Id;
                    entity.Cargo = cargo;
                }

                //ATUALIZA O TELEFONE SE VIER NA LISTA DO REQUEST
                if (request.Telefones.Count > 0)
                {
                    foreach (var item in request.Telefones)
                    {
                        //VERIFICA SE JÁ NÃO É O MESMO QUE ESTÁ CADASTRADO
                        if (!entity.Telefones.Any(e => e.Telefone == item.Telefone))
                        {
                            //VERIFICA SE JÁ EXISTEM UM TELEFONE NO BANCO EM USO
                            var telefoneResult = await _telefoneRepository.FindAsync(e => e.Telefone == item.Telefone);

                            //SE O TELEFONE NÃO TIVER ID, É UM TELEFONE NOVO. CASO CONTRÁRIO É ATUALIZADO.
                            if (item.Id == 0)
                            {
                                //ESSA CONDIÇÃO RETORNA ERRO CASO O TELEFONE ESTEJA EM USO
                                if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                                entity.Telefones.Add(item);
                            }
                            else
                            {
                                //ESSA CONDIÇÃO RETORNA ERRO CASO O TELEFONE ESTEJA EM USO
                                if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                                _telefoneRepository.Update(item);
                            }
                        }
                    }
                }

                //FAZ O SET DO USUARIO
                if (request.Nome != null) entity.Usuario.Nome = request.Nome.ToUpper();
                if (request.Email != null) entity.Usuario.Email = request.Email.ToUpper();


                // FAZ O SET DO PARCEIRO
                if (request.Nome != null) entity.Nome = request.Nome.ToUpper();
                if (request.Email != null) entity.Email = request.Email.ToUpper();
                if (request.Cnpj != null) entity.Cnpj = request.Cnpj.ToUpper();
                if (request.Descricao != null) entity.Descricao = request.Descricao.ToUpper();

                // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
                if (request.Uf != null) entity.Endereco.Uf = request.Uf.ToUpper();
                if (request.Cep != null) entity.Endereco.Cep = request.Cep.ToUpper();
                if (request.EnderecoLogradouro != null) entity.Endereco.EnderecoLogradouro = request.EnderecoLogradouro.ToUpper();
                if (request.Bairro != null) entity.Endereco.Bairro = request.Bairro.ToUpper();
                if (request.Cidade != null) entity.Endereco.Cidade = request.Cidade.ToUpper();
                if (request.Numero != null) entity.Endereco.Numero = request.Numero.ToUpper();

                _parceiroRepository.Update(entity);

                _parceiroRepository.Save();
                _parceiroRepository.Commit();

                return new PartnnerUpdateResponse
                {
                    Id = entity.Id,
                    Nome = entity.Nome,
                    Email = entity.Email,
                    Cnpj = entity.Cnpj,
                    Descricao = entity.Descricao,
                    Telefones = entity.Telefones,
                    CargoId = entity.CargoId,
                    UsuarioId = entity.UsuarioId,
                    Cargo = entity.Cargo,
                    Usuario = entity.Usuario,
                    EnderecoId = entity.EnderecoId,
                    Endereco = entity.Endereco
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
