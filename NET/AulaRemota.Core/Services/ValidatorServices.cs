using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public class ValidatorServices : IValidatorServices
    {
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public ValidatorServices(IRepository<UserModel> usuarioRepository, IRepository<PhoneModel> telefoneRepository)
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

        public bool PhoneValidator(List<PhoneModel> phoneList)
        {
            foreach (var item in phoneList)
            {
                var result = _telefoneRepository.Find(e => e.PhoneNumber == item.PhoneNumber);
                if (result != null) return false;
            }
            return true;
        }
    }
}
