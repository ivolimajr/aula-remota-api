using AulaRemota.Infra.Entity.Auto_Escola;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class EnderecoModel
    {
        public EnderecoModel()
        {
            this.Alunos = new List<AlunoModel>();
        }
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }


        [JsonIgnore]
        public virtual ParceiroModel Parceiro { get; set; }
        [JsonIgnore]
        public virtual AutoEscolaModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual AdministrativoModel Administrativo { get; set; }
        [JsonIgnore]
        public virtual InstrutorModel Instrutor { get; set; }
        [JsonIgnore]
        public virtual List<AlunoModel> Alunos { get; set; }

    }
}
