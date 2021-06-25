using AulaRemota.Core.Entity;

namespace AulaRemota.Core.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetByEmail(string email);
        Usuario CreateUsuario(Usuario usuario);
    }
}
