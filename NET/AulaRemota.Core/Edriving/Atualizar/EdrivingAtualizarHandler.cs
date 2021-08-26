using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Core.Helpers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AulaRemota.Core.Edriving.Atualizar
{
    public class EdrivingAtualizarHandler : IRequestHandler<EdrivingAtualizarInput, EdrivingAtualizarResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<EdrivingCargoModel> _cargoRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public EdrivingAtualizarHandler(
            IRepository<EdrivingModel> edrivingRepository,
            IRepository<UsuarioModel> usuarioRepository,
            IRepository<EdrivingCargoModel> cargoRepository,
            IRepository<TelefoneModel> telefoneRepository
            )
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<EdrivingAtualizarResponse> Handle(EdrivingAtualizarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                //INICIA UMA TRANSAÇÃO
                _edrivingRepository.CreateTransaction();

                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = _edrivingRepository.Context
                            .Set<EdrivingModel>()
                            .Include(e => e.Usuario)
                            .Include(e => e.Cargo)
                            .Include(e => e.Telefones)
                            .Where(e => e.Id == request.Id)
                            .FirstOrDefault();

                //SE FOR NULO RETORNA NÃO ENCONTRADO
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

                //ATUALIZA EMAIL SE INFORMADO - TANTO DO USUÁRIO COMO DO EDRIVING
                if (request.Email != null && request.Email != entity.Email)
                {
                    //VERIFICA SE O EMAIL JÁ ESTÁ EM USO POR OUTRO USUÁRIO
                    var emailUnique = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                    if (emailUnique != null && emailUnique.Id != request.Id) throw new HttpClientCustomException("Email em uso");

                    //SE NÃO ESTPA EM USO SET OS ATRIBUTOS
                    entity.Usuario.Email = request.Email.ToUpper();
                    entity.Email = request.Email.ToUpper();

                }
                //ATUALIZA NOME SE INFORMADO - TANTO DO USUÁRIO COMO DO EDRIVING
                if (request.Nome != null && request.Nome != entity.Nome)
                {
                    entity.Usuario.Nome = request.Nome.ToUpper();
                    entity.Nome = request.Nome.ToUpper();
                }
                //ATUALIZA O CPF SE INFORMADO
                if (request.Cpf != null && request.Cpf != entity.Cpf)
                {
                    //VERIFICA SE O CPF JÁ ESTÁ EM USO
                    var cpfUnique = await _edrivingRepository.FindAsync(u => u.Cpf == request.Cpf);
                    if (cpfUnique != null && cpfUnique.Id != request.Id) throw new HttpClientCustomException("Cpf já existe em nossa base de dados");

                    //SE FOR NULO ELE ATUALIZA O CPF
                    entity.Cpf = request.Cpf.ToUpper();
                }
                //SE FOR INFORMADO UM NOVO CARGO, O CARGO ATUAL SERÁ ATUALIZADO
                if (request.CargoId > 0 && request.CargoId != entity.CargoId)
                {
                    //VERIFICA SE O CARGO INFORMADO EXISTE
                    var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
                    if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

                    //SE O CARGO EXISTE, O CARGO SERÁ ATUALIZADO
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
                                if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                                entity.Telefones.Add(item);
                            }
                            else
                            {
                                //ESSA CONDIÇÃO RETORNA ERRO CASO O TELEFONE ESTEJA EM USO
                                if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                                _telefoneRepository.Update(item);
                            }
                        }
                    }
                }

                _edrivingRepository.Update(entity);

                _edrivingRepository.Save();
                _edrivingRepository.Commit();

                return new EdrivingAtualizarResponse
                {
                    Id = entity.Id,
                    Nome = entity.Nome,
                    Email = entity.Email,
                    Cpf = entity.Cpf,
                    Telefones = entity.Telefones,
                    CargoId = entity.CargoId,
                    UsuarioId = entity.UsuarioId,
                    Cargo = entity.Cargo,
                    Usuario = entity.Usuario
                };
            }
            catch (Exception e)
            {
                _edrivingRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _edrivingRepository.Context.Dispose();
            }
        }
    }
}