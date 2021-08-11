using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.AutoEscola.Criar
{
    public class AutoEscolaCriarResponse
    {
        public AutoEscolaCriarResponse()
        {
            this.Telefones = new List<TelefoneModel>();
        }
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoEstadual { get; set; }

        public DateTime DataFundacao { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string Site { get; set; }
        public string Cnpj { get; set; }

        public int CargoId { get; set; }
        public AutoEscolaCargoModel Cargo { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public virtual List<TelefoneModel> Telefones { get; set; }
    }
}
