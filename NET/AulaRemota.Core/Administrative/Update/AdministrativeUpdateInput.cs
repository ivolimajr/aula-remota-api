using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Administrative.Update
{
    public class AdministrativeUpdateInput : IRequest<AdministrativeUpdateResponse>
    {
        [Required]
        [Range(1,99999)]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        //[CpfValidador(ErrorMessage = "Cpf é Inválido")]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        public List<PhoneModel> PhonesNumbers { get; set; }

        public int LevelId { get; set; }
    }
}
