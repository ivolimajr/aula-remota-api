using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Instructor.GetOne
{
    public class InstructorGetOneInput : IRequest<InstructorModel>
    {
        [Required]
        [StringLength(maximumLength: 99999, MinimumLength = 1)]
        public int Id { get; set; }

        [StringLength(maximumLength: 2)]
        public string Uf { get; set; }
    }
}
