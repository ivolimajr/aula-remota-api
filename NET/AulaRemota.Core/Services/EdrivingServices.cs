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

        public EdrivingServices(IEdrivingRepository edrivingRepository, IUsuarioServices usuarioServices)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioServices;
        }

        Edriving IEdrivingServices.Create(EdrivingCreateRequest entity)
        {
            var user = new Usuario();

            user.FullName = entity.FullName;
            user.Email = entity.Email;
            user.NivelAcesso = 10;
            user.status = 1;
            user.Password =  BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            var edriving = new Edriving()
            {
                FullName = entity.FullName,
                Cpf = entity.Cpf,
                Email = entity.Email,
                CargoId = entity.CargoId,
                Telefone = entity.Telefone,
                Usuario = user
            };

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

        Edriving IEdrivingServices.Update(Edriving entity)
        {
            return _edrivingRepository.Update(entity);
        }

        bool IEdrivingServices.Delete(int id)
        {
            var edriving = _edrivingRepository.GetFullObjectById(id);
            var usuario = _usuarioRepository.GetById(edriving.UsuarioId);
            usuario.status = 0;
            if (_usuarioRepository.Update(usuario) != null) return true;
            return false;
        }

        bool IEdrivingServices.Ativar(int id)
        {
            var edriving = _edrivingRepository.GetFullObjectById(id);
            var usuario = _usuarioRepository.GetById(edriving.UsuarioId);
            usuario.status = 1;
            if (_usuarioRepository.Update(usuario) != null) return true;
            return false;
        }

        bool IEdrivingServices.Inativar(int id)
        {
            var edriving = _edrivingRepository.GetFullObjectById(id);
            var usuario = _usuarioRepository.GetById(edriving.UsuarioId);
            usuario.status = 2;
            if(_usuarioRepository.Update(usuario) != null) return true;
            return false;
        }
    }
}
