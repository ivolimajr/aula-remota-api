using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.DrivingSchool.GetOne
{
    public class DrivingSchoolGetOneInput : IRequest<DrivingSchoolGetOneResponse>
    {
        [Required]
        [StringLength(maximumLength: 99999, MinimumLength = 1)]
        public int Id { get; set; }
    }
}
