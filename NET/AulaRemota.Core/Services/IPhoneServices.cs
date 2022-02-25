using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public interface IPhoneServices
    {
        public bool Exists(List<PhoneModel> phoneList);
        public bool Exists(string phone);
        public bool Exists(PhoneModel phoneModel);
        //public IEnumerable<PhoneModel> Update(List<PhoneModel> phoneList);

    }
}
