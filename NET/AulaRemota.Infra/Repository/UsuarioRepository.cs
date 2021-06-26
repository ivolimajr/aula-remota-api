using AulaRemota.Core.Entity;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Data;
using System;
using System.Linq;

namespace AulaRemota.Infra.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MySqlContext context) : base(context)
        {

        }

        Usuario IUsuarioRepository.CreateUsuario(Usuario usuario)
        {
            try
            {
                _context.Set<Usuario>().Add(usuario);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return usuario;
        }

        Usuario IUsuarioRepository.GetByEmail(string email)
        {
            try
            {
                return _context.Set<Usuario>().Where(u => u.Email == email).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
