﻿using AulaRemota.Infra.Entity.Auto_Escola;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity
{
    public class ParceiroModel
    {
        public ParceiroModel()
        {
            this.Telefones = new List<TelefoneModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string Cnpj { get; set; }

        public int CargoId { get; set; }
        public ParceiroCargoModel Cargo { get; set; }
        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public virtual List<TelefoneModel> Telefones { get; set; }

    }
}
