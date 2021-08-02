using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity.Auto_Escola
{
    public class TelefoneModel
    {
        public int Id { get; set; }
        public string Telefone { get; set; }

        [JsonIgnore]
        public virtual EdrivingModel Edriving { get; set; }
        [JsonIgnore]
        public virtual ParceiroModel Parceiro { get; set; }
        [JsonIgnore]
        public virtual AutoEscolaModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual AdministrativoModel Administrativo { get; set; }
        [JsonIgnore]
        public virtual InstrutorModel Instrutor { get; set; }
        [JsonIgnore]
        public virtual AlunoModel Aluno { get; set; }
    }
}
