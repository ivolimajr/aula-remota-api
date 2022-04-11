using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Instructor.Remove
{
    public class InstructorRemoveInput : IRequest<bool>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }
    }
}
