using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using AulaRemota.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;

namespace AulaRemota.Core.Edriving.Atualizar
{
    public class EdrivingAtualizarHandler : IRequestHandler<EdrivingAtualizarInput, EdrivingAtualizarResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<EdrivingCargoModel> _cargoRepository;

        public EdrivingAtualizarHandler(IRepository<EdrivingModel> edrivingRepository, IRepository<UsuarioModel> usuarioRepository, IRepository<EdrivingCargoModel> cargoRepository)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
        }


        public async Task<EdrivingAtualizarResponse> Handle(EdrivingAtualizarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            try
            {
                UnitOfWork.Current.CreateTransaction();
                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = _edrivingRepository.GetById(request.Id);
                if (entity == null) throw new HttpClientCustomException("Não Encontrado");

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
                if (request.Nome != null) entity.Nome = request.Nome.ToUpper();
                if (request.Email != null) entity.Email = request.Email.ToUpper();
                if (request.Telefone != null) entity.Telefones = new List<TelefoneModel> { new TelefoneModel { Telefone = request.Telefone } };
                if (request.Cpf != null) entity.Cpf = request.Cpf.ToUpper();

                var resultUser = _usuarioRepository.Update(usuario);
                if (resultUser == null) throw new HttpClientCustomException("Errro ao salvar dados usuário");

                EdrivingModel edrivingModel = _edrivingRepository.Update(entity);

                var edrivingResult = new EdrivingAtualizarResponse
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

                UnitOfWork.Current.Save();
                UnitOfWork.Current.Commit();
                return edrivingResult;
            }
            catch (Exception)
            {
                UnitOfWork.Current.Rollback();
                throw;
            }
            finally
            {
                UnitOfWork.Current.Dispose();
            }
        }
    }
}
