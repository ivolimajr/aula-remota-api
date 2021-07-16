using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Entity.Parceiro.Criar
{
    public class ParceiroCriarInput : IRequest<ParceiroCriarResponse>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 8)]
        public string Cep { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string EnderecoLogradouro { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Numero { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 11)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Senha { get; set; }

        [Range(0, 1)]
        public int Status { get; set; }

        [Required]
        [Range(1, 100)]
        public int CargoId { get; set; }
    }
}
