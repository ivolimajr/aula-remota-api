using AulaRemota.Infra.Entity.DrivingSchool;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Instructor.Update
{
    public class InstructorUpdateHandlerBase
    {

        public async Task<InstructorModel> Handle(InstructorUpdateInput request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}