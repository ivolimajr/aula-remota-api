using AulaRemota.Infra.Entity.Auto_Escola;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public interface IValidatorServices
    {
        public bool EmailValidator(string email);
        public bool PhoneValidator(List<TelefoneModel> phoneList);
    }
}
