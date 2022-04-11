using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Instructor.GetAll
{
    public class InstructorGetAllInput : IRequest<List<InstructorModel>>
    {
        public string Uf { get; set; }
    }
}
