using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public class ValidatorServices : IValidatorServices
    {
        private readonly IRepository<UserModel, int>_userRepository;
        private readonly IRepository<PhoneModel, int> _phoneRepository;

        public ValidatorServices(IRepository<UserModel, int>userRepository, IRepository<PhoneModel, int> phoneRepository)
        {
            _userRepository = userRepository;
            _phoneRepository = phoneRepository;
        }

        public bool EmailValidator(string email)
        {
            var result = _userRepository.FirstOrDefault(e => e.Email.ToLower() == email.ToLower()).Email;
            if (String.IsNullOrEmpty(result)) return false;
            return true;
        }

        public bool PhoneValidator(List<PhoneModel> phoneList)
        {
            foreach (var item in phoneList)
            {
                var result = _phoneRepository.FirstOrDefault(e => e.PhoneNumber == item.PhoneNumber);
                if (result != null) return false;
            }
            return true;
        }
    }
}
