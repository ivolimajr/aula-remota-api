using System.Collections.Generic;

namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class CursoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public int CargaHoraria { get; set; }
        public string Descricao { get; set; }

        public int InstrutorId { get; set; }
        public InstrutorModel Instrutor { get; set; }

        public int TurmaId { get; set; }
        public virtual ICollection<TurmaModel> Turmas { get; set; }

        public int AutoEscolaId { get; set; }
        public virtual ICollection<AutoEscolaModel> AutoEscolas { get; set; }
    }
}
