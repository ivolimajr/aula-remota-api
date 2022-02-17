using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.AutoEscola.GetOne
{
    public class AutoEscolaGetOneInput : IRequest<AutoEscolaGetOneResponse>
    {
        [Required]
        [StringLength(maximumLength: 99999, MinimumLength = 1)]
        public int Id { get; set; }
    }
}
