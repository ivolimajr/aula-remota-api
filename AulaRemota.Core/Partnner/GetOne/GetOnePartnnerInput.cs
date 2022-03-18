using MediatR;

namespace AulaRemota.Core.Partnner.GetOne
{
    public class GetOnePartnnerInput : IRequest<GetOnePartnnerResponse>
    {
        public int Id { get; set; }
    }
}
