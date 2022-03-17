using MediatR;

namespace AulaRemota.Core.DrivingSchool.Remove
{
    public class DrivingSchoolRemoveInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
