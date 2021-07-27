using AulaRemota.Core.Entity.Auto_Escola;
using System.Collections.Generic;

namespace AulaRemota.Core.Entity
{
    public class ParceiroModel
    {
        public ParceiroModel()
        {
            this.Telefones = new List<TelefoneModel>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
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
