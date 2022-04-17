using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Administrative.GetAll
{
    public  class AdministrativeGetAllInput : IRequest<List<AdministrativeModel>>
    {
        public int DrivingSchoolId { get; set; }
    }
}
