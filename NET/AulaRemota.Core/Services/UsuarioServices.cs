using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        Usuario IUsuarioServices.Create(Usuario entity)
        {

            var userExists = _usuarioRepository.GetByEmail(entity.Email);

            if (userExists != null) return null;

            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

            return _usuarioRepository.Create(entity);
        }

        bool IUsuarioServices.Delete(int id)
        {
            var result = _usuarioRepository.GetById(id);
            return _usuarioRepository.Delete(result);
        }

        IEnumerable<Usuario> IUsuarioServices.GetAll()
        {
            return _usuarioRepository.GetAll();
        }

        Usuario IUsuarioServices.GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        Usuario IUsuarioServices.GetByEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

        IEnumerable<Usuario> IUsuarioServices.GetWhere(Expression<Func<Usuario, bool>> predicado)
        {
            return _usuarioRepository.GetWhere(predicado);
        }

        Usuario IUsuarioServices.Login(string email, string senha)
        {
            var result = _usuarioRepository.GetByEmail(email);

            if (result == null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(senha, result.Password);

            if (validPassword) return result;

            return null;
        }

        Usuario IUsuarioServices.Update(Usuario usuario)
        {
            var entity = _usuarioRepository.GetById(usuario.Id);

            if (entity == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(usuario.Password, entity.Password)) return null;

            entity.FullName = usuario.FullName;
            entity.Email = usuario.Email;

            return _usuarioRepository.Update(entity);
        }

        bool IUsuarioServices.ValidateEntity(Usuario entity)
        {
            if (entity.FullName == null ||
                entity.Email == null ||
                entity.Password == null ||
                entity.Id == 0)
                return false;

            return true;
        }
    }
}
