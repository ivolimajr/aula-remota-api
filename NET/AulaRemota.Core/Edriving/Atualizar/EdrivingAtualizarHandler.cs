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
                _edrivingRepository.CreateTransaction();

                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = _edrivingRepository.Context
                            .Set<EdrivingModel>()
                            .Include(e => e.Usuario)
                            .Include(e => e.Cargo)
                            .Include(e => e.Telefones)
                            .Where(e => e.Id == request.Id)
                            .Where(e => e.Usuario.status > 0)
                            .FirstOrDefault();


                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

                if(request.Nome != null || request.Email != null || request.Cpf != null)
                {
                    //BUSCA O USUARIO PARA ATUALIZAR
                    var usuario = await _usuarioRepository.GetByIdAsync(entity.UsuarioId);
                    if (usuario == null) throw new HttpClientCustomException("Errro ao carregar usuário");

                    //ATUALIZA EMAIL SE NÃO FOR NULO
                    if (request.Email != null && request.Email != entity.Email)
                    {
                        //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                        var emailUnique = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                        if (emailUnique != null && emailUnique.Id != request.Id) throw new HttpClientCustomException("Email em uso");

                        usuario.Email = request.Email.ToUpper();
                        entity.Email = request.Email.ToUpper();

                    }
                    //ATUALIZA NOME SE NÃO FOR NULO
                    if (request.Nome != null && request.Nome != entity.Nome)
                    {
                        usuario.Nome = request.Nome.ToUpper();
                        entity.Nome = request.Nome.ToUpper();
                    }
                    //ATUALIZA O CPF SE NAO FOR NULO
                    if (request.Cpf != null && request.Cpf != entity.Cpf)
                    {
                        //VERIFICA SE O CPF JÁ ESTÁ EM USO
                        var cpfUnique = await _edrivingRepository.FindAsync(u => u.Cpf == request.Cpf);
                        if (cpfUnique != null && cpfUnique.Id != request.Id) throw new HttpClientCustomException("Cpf já existe em nossa base de dados");

                        entity.Cpf = request.Cpf.ToUpper();
                    }

                    //ATUALIZA O USUARIO
                    var resultUser = _usuarioRepository.Update(usuario);
                    if (resultUser == null) throw new HttpClientCustomException("Errro ao salvar dados usuário");
                }


                //SE FOR INFORMADO UM NOVO CARGO, O CARGO ATUAL SERÁ ATUALIZADO
                if (request.CargoId > 0 && request.CargoId != entity.CargoId)
                {
                    var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
                    if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

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
                                if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                                entity.Telefones.Add(item);
                            }
                            else
                            {
                                //BUSCA O TELEFONE NO BANCO PARA COMPARAR SE TEVE MUDANÇAS, SE TIVER MUDANÇA ELE ATUALIZA. ASSIM EVITA UM SELECT NA BASE TODA.
                                var telefoneDb = await _telefoneRepository.GetByIdAsync(item.Id);
                                if (telefoneDb.Telefone != item.Telefone)
                                {
                                    if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                                    _telefoneRepository.Update(item);
                                }
                            }
                        }
                    }
                }

                var edrivingModel = _edrivingRepository.Update(entity);

                _edrivingRepository.Save();
                _edrivingRepository.Commit();

                return new EdrivingAtualizarResponse
                {
                    Id = edrivingModel.Id,
                    Nome = edrivingModel.Nome,
                    Email = edrivingModel.Email,
                    Cpf = edrivingModel.Cpf,
                    Telefones = edrivingModel.Telefones,
                    CargoId = edrivingModel.CargoId,
                    UsuarioId = edrivingModel.UsuarioId,
                    Cargo = edrivingModel.Cargo,
                    Usuario = edrivingModel.Usuario
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
