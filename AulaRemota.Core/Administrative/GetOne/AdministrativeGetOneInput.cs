using AulaRemota.Infra.Entity.DrivingSchool;
using MediatR;

namespace AulaRemota.Core.Administrative.GetOne
{
    public class AdministrativeGetOneInput : IRequest<AdministrativeModel>
    {
        public int Id { get; set; }
    }
}
