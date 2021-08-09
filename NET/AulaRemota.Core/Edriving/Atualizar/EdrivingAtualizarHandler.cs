using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Core.Helpers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity.Auto_Escola;

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
                var entity = _edrivingRepository.GetById(request.Id);
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

                if(request.Email != null && request.Email != entity.Email)
                {
                    //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                    var emailUnique = _usuarioRepository.Find(u => u.Email == request.Email);
                    if (emailUnique != null && emailUnique.Id != request.Id) throw new HttpClientCustomException("Email em uso");
                }

                if (request.Cpf != null && request.Cpf != request.Cpf)
                {

                }

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfUnique = _edrivingRepository.Find(u => u.Cpf == request.Cpf);
                if (cpfUnique != null && cpfUnique.Id != request.Id) throw new HttpClientCustomException("Cpf já existe em nossa base de dados");

                //ATUALIZA O TELEFONE
                if (request.Telefones.Count > 0)
                {
                    foreach (var item in request.Telefones)
                    {
                        //BUSCA O TELEFONE NO BANCO PARA COMPARAR SE TEVE MUDANÇAS
                        if (item.Id == 0)
                        {
                            var telefoneResult = await _telefoneRepository.FindAsync(e => e.Telefone == item.Telefone);
                            if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                            entity.Telefones.Add(item);
                        }
                        else
                        {
                            var telefone = await _telefoneRepository.GetByIdAsync(item.Id);
                            if (telefone.Telefone != item.Telefone)
                            {
                                var telefoneResult = _telefoneRepository.Find(e => e.Telefone == item.Telefone);
                                if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");

                                _telefoneRepository.Update(item);
                            }
                        }

                    }
                }

                //SE FOR INFORMADO UM NOVO CARGO, O CARGO ATUAL SERÁ ATUALIZADO
                if (request.CargoId != 0)
                {
                    var cargo = await _cargoRepository.GetByIdAsync(request.CargoId);
                    if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

                    //SE O CARGO EXISTE, O OBJETO SERÁ ATUALIZADO
                    entity.CargoId = cargo.Id;
                    entity.Cargo = cargo;
                }
                else
                {
                    //SE O REQUEST NÃO INFORMAR UM CARGO, É SETADO O CARGO ANTERIOR
                    var cargo = await _cargoRepository.GetByIdAsync(entity.CargoId);
                    if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

                    entity.Cargo = cargo;
                }

                //BUSCA O USUARIO PARA ATUALIZAR
                var usuario = await _usuarioRepository.GetByIdAsync(entity.UsuarioId);
                if (usuario == null) throw new HttpClientCustomException("Errro ao carregar usuário");
                //ATUALIZA O USUARIO
                if (request.Nome != null) usuario.Nome = request.Nome.ToUpper();
                if (request.Email != null) usuario.Email = request.Email.ToUpper();

                var resultUser = _usuarioRepository.Update(usuario);
                if (resultUser == null) throw new HttpClientCustomException("Errro ao salvar dados usuário");


                // ATUALIZA O USUARIO-EDRIVING
                if (request.Nome != null) entity.Nome = request.Nome.ToUpper();
                if (request.Email != null) entity.Email = request.Email.ToUpper();
                if (request.Cpf != null) entity.Cpf = request.Cpf.ToUpper();

                EdrivingModel edrivingModel = _edrivingRepository.Update(entity);

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
