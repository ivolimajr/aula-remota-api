using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class TurmaModel
    {
        public TurmaModel()
        {
            this.Alunos = new List<StudentModel>();
            this.Cursos = new List<CourseModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Codigo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int AutoEscolaId { get; set; }
        public DrivingSchoolModel AutoEscola { get; set; }

        public int InstrutorId { get; set; }
        public InstructorModel Instrutor { get; set; }

        public virtual List<StudentModel> Alunos { get; set; }
        public virtual List<CourseModel> Cursos { get; set; }
    }
}
