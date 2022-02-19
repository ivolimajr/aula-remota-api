using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class CourseModel
    {
        public CourseModel()
        {
            this.Turmas = new List<TurmaModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Codigo { get; set; }
        public int CargaHoraria { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }
        
        public int InstrutorId { get; set; }
        public InstructorModel Instrutor { get; set; }

        public int AutoEscolaId { get; set; }
        public virtual DrivingSchoolModel AutoEscolas { get; set; }

        public virtual List<TurmaModel> Turmas { get; set; }
    }
}
