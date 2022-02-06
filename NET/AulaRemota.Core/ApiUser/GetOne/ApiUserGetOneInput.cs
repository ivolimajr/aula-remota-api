using AulaRemota.Infra.Entity.Auth;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.ApiUser.GetOne
{
    public class ApiUserGetOneInput : IRequest<ApiUserModel>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }
    }
}
