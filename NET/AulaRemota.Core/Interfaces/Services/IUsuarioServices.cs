using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IUsuarioServices
    {
        Usuario Create(Usuario entity);

        Usuario Update(Usuario entity);

        IEnumerable<Usuario> GetAll();

        Usuario GetById(int id);

        Usuario GetByEmail(string email);

        IEnumerable<Usuario> GetWhere(Func<Usuario, bool> predicado);

        void Delete(int id);

        Usuario Login(string email, string senha);
        bool ValidateEntity(Usuario entity);
    }
}
