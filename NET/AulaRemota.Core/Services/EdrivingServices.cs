using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AulaRemota.Api.Models.Requests;

namespace AulaRemota.Core.Services
{
    public class EdrivingServices : IEdrivingServices
    {
        private readonly IEdrivingRepository _edrivingRepository;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IEdrivingCargoServices _edrivingCargoServices;

        public EdrivingServices(IEdrivingRepository edrivingRepository, IUsuarioServices usuarioServices, IEdrivingCargoServices edrivingCargoServices)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioServices = usuarioServices;
            _edrivingCargoServices = edrivingCargoServices;
        }

        Edriving IEdrivingServices.Create(EdrivingCreateRequest entity)
        {

            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            var emailResult = _usuarioServices.GetByEmail(entity.Email);
            if (emailResult != null) return null;

            //VERIFICA SE O CARGO INFORMADO EXISTE
            var cargo = _edrivingCargoServices.GetById(entity.CargoId);
            if (cargo == null) return null;

            //CRIA USUARIO
            var user = new Usuario();

            user.FullName = entity.FullName.ToUpper();
            user.Email = entity.Email.ToUpper();
            user.NivelAcesso = 10;
            user.status = entity.Status;
            user.Password =  BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //CRIA UM EDRIVING
            var edriving = new Edriving()
            {
                FullName = entity.FullName.ToUpper(),
                Cpf = entity.Cpf.ToUpper(),
                Email = entity.Email.ToUpper(),
                CargoId = entity.CargoId,
                Telefone = entity.Telefone.ToUpper(),
                Cargo = cargo,
                Usuario = user
            };

            //CRIA A ENTIDADE
            return _edrivingRepository.Create(edriving);
        }

        IEnumerable<Edriving> IEdrivingServices.GetAll()
        {
            var result = _edrivingRepository.GetAllWithRelationship();
            if (result == null) return null;
            return result;

        }

        IEnumerable<Edriving> IEdrivingServices.GetAllWithRelationship()
        {
            return _edrivingRepository.GetAllWithRelationship();
        }

        Edriving IEdrivingServices.GetById(int id)
        {
            //BUSCA O OBJETO A SER RETORNADO
            var result = _edrivingRepository.GetById(id);
            if (result == null) return null;

            //MONTA O CARGO E USUARIO
            result.Cargo = _edrivingCargoServices.GetById(result.CargoId);
            result.Usuario = _usuarioServices.GetById(result.UsuarioId);

            return result;
        }

        IEnumerable<Edriving> IEdrivingServices.GetWhere(Expression<Func<Edriving, bool>> predicado)
        {
            var result = _edrivingRepository.GetWhere(predicado);
            if (result == null) return null;

            return result;
        }

        Edriving IEdrivingServices.Update(EdrivingUpdateRequest edriving)
        {
            //BUSCA O OBJETO A SER ATUALIZADO
            var entity = _edrivingRepository.GetById(edriving.Id);
            if (entity == null) return null;

            //SE FOR INFORMADO UM NOVO CARGO, O CARGO ATUAL SERÁ ATUALIZADO
            if (edriving.CargoId != 0)
            {
                var cargo = _edrivingCargoServices.GetById(edriving.CargoId);
                if (cargo == null) return null;

                //SE O CARGO EXISTE, O OBJETO SERÁ ATUALIZADO
                entity.CargoId = cargo.Id;
                entity.Cargo = cargo;
            } else
            {
                //SE O USUÁRIO NÃO INFORMAR UM CARGO, É SETADO O CARGO ANTERIOR
                var cargo = _edrivingCargoServices.GetById(entity.CargoId);
                if (cargo == null) return null;

                entity.Cargo = cargo;
            }

            //BUSCA O OBJETO USUARIO PARA ATUALIZAR
            var usuario = _usuarioServices.GetById(entity.UsuarioId);
            if (usuario == null) return null;

            //ATUALIZA O NOME E EMAIL
            if(edriving.FullName != null)   usuario.FullName = edriving.FullName.ToUpper();
            if(edriving.Email != null)      usuario.Email = edriving.Email.ToUpper();

            var result = _usuarioServices.Update(usuario);
            if (result == null) return null;

            // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
            if (edriving.FullName != null)  entity.FullName = edriving.FullName.ToUpper();
            if (edriving.Email != null)     entity.Email = edriving.Email.ToUpper();
            if (edriving.Telefone != null)  entity.Telefone = edriving.Telefone.ToUpper();
            if (edriving.Cpf != null)       entity.Cpf = edriving.Cpf.ToUpper();

            return _edrivingRepository.Update(entity);
        }

        bool IEdrivingServices.Delete(int id)
        {
            //BUSCA O OBJETO A SER REMOVIDO
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            //REMOVE O OBJETO
            bool resEdriving = _edrivingRepository.Delete(edriving);
            if (!resEdriving) return false;

            //REMOVE O USUARIO
            bool resUsuario = _usuarioServices.Delete(edriving.UsuarioId);
            if (!resUsuario) return false;

            return true;
            
        }

        bool IEdrivingServices.Ativar(int id)
        {
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            var usuario = _usuarioServices.GetById(edriving.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 1;

            if (_usuarioServices.Update(usuario) != null) return true;
            return false;
        }

        bool IEdrivingServices.Inativar(int id)
        {
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            var usuario = _usuarioServices.GetById(edriving.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 2;

            if (_usuarioServices.Update(usuario) != null) return true;
            return false;
        }
    }
}
