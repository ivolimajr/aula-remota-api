using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.DrivingSchool.GetAll
{
    public class DrivingSchoolGetAllInput : IRequest<List<DrivingSchoolModel>>
    {
    }
}
