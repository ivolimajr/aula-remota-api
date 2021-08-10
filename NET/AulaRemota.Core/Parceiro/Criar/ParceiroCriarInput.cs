using AulaRemota.Core.Parceiro.Criar;
using AulaRemota.Infra.Entity.Auto_Escola;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Parceiro.Criar
{
    public class ParceiroCriarInput : IRequest<ParceiroCriarResponse>
    {
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nome { get; set; }

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

        public List<TelefoneModel> Telefones { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Senha { get; set; }

        [Required]
        [Range(1, 100)]
        public int CargoId { get; set; }
    }
}
