using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public interface IValidatorServices
    {
        public bool EmailValidator(string email);
        public bool PhoneValidator(List<PhoneModel> phoneList);
    }
}
