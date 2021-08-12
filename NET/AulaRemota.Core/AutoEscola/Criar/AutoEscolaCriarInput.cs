using AulaRemota.Infra.Entity.Auto_Escola;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AulaRemota.Core.AutoEscola.Criar
{
    public class AutoEscolaCriarInput : IRequest<AutoEscolaCriarResponse>
    {
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string RazaoSocial { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string NomeFantasia { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 20)]
        public string InscricaoEstadual { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFundacao { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        public string Email { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 0)]
        public string Descricao { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Site { get; set; }

        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Senha { get; set; }

        [Required]
        [Range(1,100)]
        public int CargoId { get; set; }


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

        public List<TelefoneModel> Telefones { get; set; }

        [Required]
        public IFormFile Arquivo { get; set; }

    }
}
