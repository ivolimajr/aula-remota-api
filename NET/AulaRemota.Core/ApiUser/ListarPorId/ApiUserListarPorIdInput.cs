using AulaRemota.Infra.Entity.Auth;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserListarPorIdInput : IRequest<ApiUserModel>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }
    }
}
