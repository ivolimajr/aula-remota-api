using AulaRemota.Infra.Entity.Auto_Escola;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Models
{
    public class ArquivoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Formato { get; set; }
        public string Destino { get; set; }

        [JsonIgnore]
        public virtual AutoEscolaModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual InstrutorModel Instrutor { get; set; }
    }
}
