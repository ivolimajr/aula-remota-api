using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.DrivingSchool.Remove
{
    public class DrivingSchoolRemoveInput : IRequest<bool>
    {
        [Required]
        [Range(1,99999)]
        public int Id { get; set; }
    }
}
