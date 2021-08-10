using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity.Auto_Escola
{
    public class AutoEscolaCargoModel
    {
        public int Id { get; set; }
        public string Cargo { get; set; }

        [JsonIgnore]
        public virtual List<AutoEscolaModel> AutoEscolas { get; set; }

        [JsonIgnore]
        public virtual List<InstrutorModel> Instrutores { get; set; }

        [JsonIgnore]
        public virtual List<AdministrativoModel> Administrativos { get; set; }
    }
}
