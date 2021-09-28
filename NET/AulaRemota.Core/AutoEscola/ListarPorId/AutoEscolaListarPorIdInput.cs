using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.AutoEscola.ListarPorId
{
    public class AutoEscolaListarPorIdInput : IRequest<AutoEscolaListarPorIdResponse>
    {
        [Required]
        [StringLength(maximumLength: 99999, MinimumLength = 1)]
        public int Id { get; set; }
    }
}
