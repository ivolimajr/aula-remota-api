using System.Collections.Generic;

namespace AulaRemota.Infra.Entity.Auto_Escola
{
    public class AutoEscolaCargoModel
    {
        public int Id { get; set; }
        public string Cargo { get; set; }

        public virtual List<AutoEscolaModel> AutoEscolas { get; set; }
        public virtual List<InstrutorModel> Instrutores { get; set; }
        public virtual List<AdministrativoModel> Administrativos { get; set; }
    }
}
