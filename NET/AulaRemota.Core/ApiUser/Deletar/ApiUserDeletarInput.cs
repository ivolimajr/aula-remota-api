using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.ApiUser.Criar
{
    public class ApiUserDeletarInput : IRequest<bool>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }
    }
}
