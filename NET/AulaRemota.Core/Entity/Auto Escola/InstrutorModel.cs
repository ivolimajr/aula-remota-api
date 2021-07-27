using AulaRemota.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class InstrutorModel
    {
        public InstrutorModel()
        {
            this.Telefones = new List<TelefoneModel>();
            this.Cursos = new List<CursoModel>();
            this.AutoEscolas = new List<AutoEscolaModel>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Identidade { get; set; }
        public string Orgão { get; set; }
        public DateTime Aniversario { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }
        public int CargoId { get; set; }
        public AutoEscolaCargoModel Cargo { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }


        public virtual List<AutoEscolaModel> AutoEscolas { get; set; }
        public List<CursoModel> Cursos { get; set; }
        public List<TelefoneModel> Telefones { get; set; }
        public virtual List<ArquivoModel> Arquivos { get; set; }
    }
}