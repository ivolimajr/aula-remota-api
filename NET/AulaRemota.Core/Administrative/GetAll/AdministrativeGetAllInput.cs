using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.GetAll
{
    public  class AdministrativeGetAllInput : IRequest<List<AdministrativeModel>>
    {
    }
}
