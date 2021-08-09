using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using System.Threading.Tasks;

namespace AulaRemota.Core.Helpers.Validadores
{
    public class AtributosUnicos
    {
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public AtributosUnicos(IRepository<UsuarioModel> usuarioRepository, IRepository<TelefoneModel> telefoneRepository)
        {
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<bool> EmailUnico(string email)
        {
            var result = await _usuarioRepository.FindAsync(e => e.Email == email);
            if (result == null) return true;
            return false;
        }
        public async Task<bool> TelefoneUnico(string telefone)
        {
            var result = await _telefoneRepository.FindAsync(e => e.Telefone == telefone);
            if (result == null) return true;
            return false;
        }
    }
}
