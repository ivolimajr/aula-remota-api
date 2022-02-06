using AulaRemota.Infra.Entity.Auto_Escola;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "varchar(2)")]
        public string Uf { get; set; }
        [Column(TypeName = "varchar(12)")]
        public string Cep { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string EnderecoLogradouro { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Bairro { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Cidade { get; set; }
        [Column(TypeName = "varchar(50)")]
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
