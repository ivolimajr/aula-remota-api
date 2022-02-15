using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.AutoEscola.Atualizar
{
    public class AutoEscolaAtualizarInput : IRequest<AutoEscolaAtualizarResponse>
    {
        [Required]
        [Range(1, 99999)]
        public int Id { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string RazaoSocial { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string NomeFantasia { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 12)]
        public string InscricaoEstadual { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFundacao { get; set; }

        [StringLength(maximumLength: 70, MinimumLength = 5)]
        public string Email { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 0)]
        public string Descricao { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Site { get; set; }

        [StringLength(maximumLength: 14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Uf { get; set; }

        [StringLength(maximumLength: 8, MinimumLength = 8)]
        public string Cep { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string EnderecoLogradouro { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Bairro { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 3)]
        public string Cidade { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Numero { get; set; }

        public List<TelefoneModel> Telefones { get; set; }
        
        public List<IFormFile> Arquivos { get; set; }
    }
}
