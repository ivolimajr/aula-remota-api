using AulaRemota.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AulaRemota.Core.Interfaces.Services
{
    public interface IUsuarioServices
    {
        Usuario Create(Usuario entity);

        Usuario Update(Usuario entity);

        IEnumerable<Usuario> GetAll();

        Usuario GetById(int id);

        IEnumerable<Usuario> GetWhere(Expression<Func<Usuario, bool>> predicado);

        bool Delete(int id);

        Usuario Login(string email, string senha);
    }
}
