using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Services.Phone.UpdatePhone
{
    public class PhoneUpdateInput :IRequest<bool>
    {
        public ICollection<PhoneModel> CurrentPhoneList { get; set; }
        public ICollection<PhoneModel> RequestPhoneList { get; set; }
    }
}
