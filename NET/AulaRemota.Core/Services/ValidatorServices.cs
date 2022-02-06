using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Repository;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public class ValidatorServices : IValidatorServices
    {
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public ValidatorServices(IRepository<UsuarioModel> usuarioRepository, IRepository<TelefoneModel> telefoneRepository)
        {
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
        }

        public bool EmailValidator(string email)
        {
            var result = _usuarioRepository.Find(e => e.Email.ToLower() == email.ToLower()).Email;
            if (String.IsNullOrEmpty(result)) return false;
            return true;
        }

        public bool PhoneValidator(List<TelefoneModel> phoneList)
        {
            foreach (var item in phoneList)
            {
                var result = _telefoneRepository.Find(e => e.Telefone == item.Telefone);
                if (result != null) return false;
            }
            return true;
        }
    }
}
