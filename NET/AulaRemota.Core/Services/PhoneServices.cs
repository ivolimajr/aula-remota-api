using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using System.Collections.Generic;

namespace AulaRemota.Core.Services
{
    public class PhoneServices : IPhoneServices
    {
        private readonly IRepository<PhoneModel, int> _phoneRepository;

        public PhoneServices(IRepository<PhoneModel, int> phoneRepository) =>
            _phoneRepository = phoneRepository;

        public bool Exists(List<PhoneModel> phoneList)
        {
            if (phoneList != null && phoneList.Count > 0)
                foreach (PhoneModel phone in phoneList)
                    return Exists(phone.PhoneNumber);

            return false;
        }
        public bool Exists(PhoneModel phoneModel) => Exists(phoneModel.PhoneNumber);
        public bool Exists(string phone)
        {
            phone = CleanNumber(phone);
            return _phoneRepository.Exists(e => e.PhoneNumber.Equals(phone));
        }
        private static string CleanNumber(string phone) => phone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
    }
}
