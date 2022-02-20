using AulaRemota.Infra.Entity;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.User.UpdateAddress
{
    public class UserAddressUpdateInput : IRequest<AddressModel>
    {
        [Required]
        public int Id { get; set; }

        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [StringLength(maximumLength: 8, MinimumLength = 8)]
        public string Cep { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Address { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string District { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string City { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Number { get; set; }
    }
}
