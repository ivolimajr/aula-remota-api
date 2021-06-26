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
        private readonly IUsuarioServices _usuarioRepository;
        private readonly IEdrivingCargoServices _edrivingCargoRepository;

        public EdrivingServices(IEdrivingRepository edrivingRepository, IUsuarioServices usuarioServices, IEdrivingCargoServices edrivingCargoRepository)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioServices;
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        Edriving IEdrivingServices.Create(EdrivingCreateRequest entity)
        {
            //VERIFICA SE O CARGO INFORMADO EXISTE
            var cargo = _edrivingCargoRepository.GetById(entity.CargoId);
            if (cargo == null) return null;

            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            var emailResult = _usuarioRepository.GetByEmail(entity.Email);
            if (emailResult != null) return null;

            //CRIA USUARIO
            var user = new Usuario();

            user.FullName = entity.FullName;
            user.Email = entity.Email;
            user.NivelAcesso = 10;
            user.status = 1;
            user.Password =  BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //CRIA UM EDRIVING
            var edriving = new Edriving()
            {
                FullName = entity.FullName,
                Cpf = entity.Cpf,
                Email = entity.Email,
                CargoId = entity.CargoId,
                Telefone = entity.Telefone,
                Cargo = cargo,
                Usuario = user
            };

            //CRIA A ENTIDADE
            return _edrivingRepository.Create(edriving);
        }

        IEnumerable<Edriving> IEdrivingServices.GetAll()
        {
            return _edrivingRepository.GetAll();
        }

        IEnumerable<Edriving> IEdrivingServices.GetAllWithRelationship()
        {
            return _edrivingRepository.GetAllWithRelationship();
        }

        Edriving IEdrivingServices.GetById(int id)
        {
            return _edrivingRepository.GetById(id);
        }

        IEnumerable<Edriving> IEdrivingServices.GetWhere(Expression<Func<Edriving, bool>> predicado)
        {
            return _edrivingRepository.GetWhere(predicado);
        }

        Edriving IEdrivingServices.Update(EdrivingCreateRequest edriving)
        {
            var entity = _edrivingRepository.GetById(edriving.Id);

            entity.FullName = edriving.FullName;
            entity.Email = edriving.Email;
            entity.Telefone = edriving.Telefone;
            entity.Cpf = edriving.Cpf;
            entity.CargoId = edriving.CargoId;

            return _edrivingRepository.Update(entity);
        }

        bool IEdrivingServices.Delete(int id)
        {
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            var usuario = _usuarioRepository.GetById(edriving.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 0;

            if (_usuarioRepository.Update(usuario) != null) return true;
            return false;
        }

        bool IEdrivingServices.Ativar(int id)
        {
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            var usuario = _usuarioRepository.GetById(edriving.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 1;

            if (_usuarioRepository.Update(usuario) != null) return true;
            return false;
        }

        bool IEdrivingServices.Inativar(int id)
        {
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            var usuario = _usuarioRepository.GetById(edriving.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 2;

            if (_usuarioRepository.Update(usuario) != null) return true;
            return false;
        }

        bool IEdrivingServices.ValidateEntity(EdrivingCreateRequest entity)
        {
            if( entity.Cpf      == null ||
                entity.FullName == null ||
                entity.Email    == null ||
                entity.Senha    == null ||
                entity.Telefone == null ||
                entity.CargoId  == 0)
                return false;

            return true;
        }
    }
}
