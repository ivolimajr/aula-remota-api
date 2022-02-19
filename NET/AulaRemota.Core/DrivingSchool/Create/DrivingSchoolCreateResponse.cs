using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Models;
using System;
using System.Collections.Generic;

namespace AulaRemota.Core.DrivingSchool.Create
{
    public class DrivingSchoolCreateResponse
    {
        public DrivingSchoolCreateResponse()
        {
            this.Telefones = new List<PhoneModel>();
            this.Arquivos = new List<FileModel>();
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

        public int EnderecoId { get; set; }
        public AddressModel Endereco { get; set; }

        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }
        public virtual List<PhoneModel> Telefones { get; set; }
        public virtual List<FileModel> Arquivos { get; set; }
    }
}
