using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.Auto_Escola
{
    public class AdministrativoModel
    {
        public AdministrativoModel()
        {
            this.Telefones = new List<TelefoneModel>();
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
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public int AutoEscolaId { get; set; }
        public AutoEscolaModel AutoEscola { get; set; }

        public ICollection<TelefoneModel> Telefones { get; set; }
    }
}
