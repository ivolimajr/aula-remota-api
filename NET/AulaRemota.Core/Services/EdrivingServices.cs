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
        private readonly IEdrivingCargoServices _edrivingCargoRepository;

        public EdrivingServices(IEdrivingRepository edrivingRepository, IUsuarioServices usuarioServices, IEdrivingCargoServices edrivingCargoRepository)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioServices = usuarioServices;
            _edrivingCargoRepository = edrivingCargoRepository;
        }

        Edriving IEdrivingServices.Create(EdrivingCreateRequest entity)
        {
            //VERIFICA SE O CARGO INFORMADO EXISTE
            var cargo = _edrivingCargoRepository.GetById(entity.CargoId);
            if (cargo == null) return null;

            //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
            var emailResult = _usuarioServices.GetByEmail(entity.Email);
            if (emailResult != null) return null;

            //CRIA USUARIO
            var user = new Usuario();

            user.FullName = entity.FullName.ToUpper();
            user.Email = entity.Email.ToUpper();
            user.NivelAcesso = 10;
            user.status = 1;
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

        IEnumerable<EdrivingGetAllRequest> IEdrivingServices.GetAll()
        {
            var result = _edrivingRepository.GetAllWithRelationship();

            var listaEdriving = new List<EdrivingGetAllRequest>();

            foreach (var item in result)
            {
                var output = new EdrivingGetAllRequest();
                output.Id = item.Id;
                output.FullName = item.FullName;
                output.Cpf = item.Cpf;
                output.Email = item.Email;
                output.Telefone = item.Telefone;
                output.Cargo = item.Cargo.Cargo;
                output.NivelAcesso = item.Usuario.NivelAcesso;
                output.Status = item.Usuario.status;

                listaEdriving.Add(output);
            }

            return listaEdriving;
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
            if (entity == null) return null;

            var usuario = _usuarioServices.GetById(entity.UsuarioId);
            if (usuario == null) return null;

            usuario.FullName = edriving.FullName.ToUpper();
            usuario.Email = edriving.Email.ToUpper();

            var result = _usuarioServices.Update(usuario);
            if (result == null) return null;

            entity.FullName = edriving.FullName.ToUpper();
            entity.Email = edriving.Email.ToUpper();
            entity.Telefone = edriving.Telefone.ToUpper();
            entity.Cpf = edriving.Cpf.ToUpper();
            entity.CargoId = edriving.CargoId;

            return _edrivingRepository.Update(entity);
        }

        bool IEdrivingServices.Delete(int id)
        {
            var edriving = _edrivingRepository.GetById(id);
            if (edriving == null) return false;

            var usuario = _usuarioServices.GetById(edriving.UsuarioId);
            if (usuario == null) return false;
            usuario.status = 0;

            if (_usuarioServices.Update(usuario) != null) return true;
            return false;
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
