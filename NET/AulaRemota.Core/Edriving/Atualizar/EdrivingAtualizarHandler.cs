using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Core.Helpers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
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
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                _edrivingRepository.CreateTransaction();
                //BUSCA O OBJETO A SER ATUALIZADO
                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailUnique = _usuarioRepository.Find(u => u.Email == request.Email);
                if (emailUnique != null && emailUnique.Id != request.UsuarioId) throw new HttpClientCustomException("Email em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfUnique = _edrivingRepository.Find(u => u.Cpf == request.Cpf);
                if (cpfUnique != null && cpfUnique.Id != request.Id) throw new HttpClientCustomException("Cpf já existe em nossa base de dados");

                var entity = _edrivingRepository.GetById(request.Id);
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");
/*
                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.Telefones.ToList())
                {
                    var telefoneResult = _telefoneRepository.Find(u => u.Telefone == item.Telefone);
                    if (telefoneResult != null && entity.Id != request.Id) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                }
*/

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
                    //SE O USUÁRIO NÃO INFORMAR UM CARGO, É SETADO O CARGO ANTERIOR
                    var cargo = await _cargoRepository.GetByIdAsync(entity.CargoId);
                    if (cargo == null) throw new HttpClientCustomException("Cargo Não Encontrado");

                    entity.Cargo = cargo;
                }

                //BUSCA O OBJETO USUARIO PARA ATUALIZAR
                var usuario = await _usuarioRepository.GetByIdAsync(entity.UsuarioId);
                if (usuario == null) throw new HttpClientCustomException("Errro ao carregar usuário");

                //ATUALIZA O NOME E EMAIL
                if (request.Nome != null) usuario.Nome = request.Nome.ToUpper();
                if (request.Email != null) usuario.Email = request.Email.ToUpper();


                // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
                if (request.Nome != null) entity.Nome = request.Nome.ToUpper();
                if (request.Email != null) entity.Email = request.Email.ToUpper();
                if (request.Telefones != null) entity.Telefones = request.Telefones;
                if (request.Cpf != null) entity.Cpf = request.Cpf.ToUpper();

                var resultUser = _usuarioRepository.Update(usuario);
                if (resultUser == null) throw new HttpClientCustomException("Errro ao salvar dados usuário");

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
            catch (Exception)
            {
                _edrivingRepository.Rollback();
                throw;
            }
            finally
            {
                _edrivingRepository.Context.Dispose();
            }
        }
    }
}
