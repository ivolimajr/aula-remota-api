using System.Collections.Generic;

namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class TurmaModel
    {
        public TurmaModel()
        {
            this.AutoEscola = new AutoEscolaModel();
        }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool Status { get; set; }
        public int AutoEscolaId { get; set; }
        public AutoEscolaModel AutoEscola { get; set; }

        public virtual ICollection<AlunoModel> Alunos { get; set; }
        public virtual ICollection<CursoModel> Cursos { get; set; }
    }
}
